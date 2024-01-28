using ApplicationDataUtilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using static PalworldServerManager.ProgramConstants;
using System.Threading.Tasks;
using PalworldServerManager.SteamCmdUtils;

namespace PalworldServerManager
{
    public partial class MainForm : Form
    {
        public UserSettings userSettings = new UserSettings();
        public SteamCmd steamCmd = null;
        public List<KnownServer> knownServers;

        private static MainForm instance = null;

        private Thread addServerThread = null;
        private KnownServer currentAddingServer = null;
        private object addServerComplete = false;
        private bool isAdding = false;

        private Thread importServerThread = null;
        private KnownServer currentImportingServer = null;
        private object importServerComplete = false;

        public static MainForm GetInstance()
        {
            return instance;
        }

        public bool DoesServerNameExist(string serverName)
        {
            foreach(KnownServer server in knownServers)
            {
                if(server.ServerName == serverName)
                {
                    return true;
                }
            }

            return false;
        }

        private static string GetFullServerPath(KnownServer server)
        {
            return server.ServerPath + DEFAULT_PAL_SERVER_DIR_NAME + " - " + server.ServerName;
        }

        private void Update(object sender, EventArgs e)
        {
            CheckAddServerThread();
            CheckImportServerThread();

            UpdateServerStatus();
        }

        public MainForm()
        {
            if(instance == null)
            {
                instance = this;
            }

            InitializeComponent();

#if DEBUG
            // Only in dev environment where build changes frequently. Release builds will always come with Data dir.
            if(!CSVDataHelper.DoesDataDirectoryExist(APPLICATION_DATA_PATH))
            {
                Dictionary<string, FileStream> dataFiles = CSVDataHelper.CreateInitialDataFiles(APPLICATION_DATA_PATH, new string[] { KNOWN_SERVERS_FILENAME, USER_SETTINGS_FILENAME });
                WriteInitialData(dataFiles);
            }
#endif

            // setup user-specific AppData path to store known servers and user settings
            if (!Directory.Exists(APPLICATION_USER_DATA_PATH))
            {
                SetupUserAppData();
            }

            userSettings.ReadUserSettings(APPLICATION_USER_DATA_PATH + USER_SETTINGS_FILENAME);

            LoadCSVOnDataGridView(KNOWN_SERVER_PATH);
            dataGridView1.CellContextMenuStripNeeded += dataGridView1_CellContextMenuStripNeeded;
            versionToolStrip.MouseHover += new EventHandler(versionToolStrip_Click);

            LoadRunningServers();

            // Ensure steamcmd install still exists, otherwise force them to retry setup
            if (userSettings.userSettingsDict["steamCmdInstallDir"] == "" || !Directory.Exists(userSettings.userSettingsDict["steamCmdInstallDir"]))
            {
                userSettings.userSettingsDict["steamCmdInstallDir"] = "";
                userSettings.userSettingsDict["completedSetup"] = "false";
            }

            if (userSettings.userSettingsDict["completedSetup"] == "false")
            {
                HandleInitialSetup();
            }

            SetupSteamCmd();

            System.Windows.Forms.Timer updater = new System.Windows.Forms.Timer();
            updater.Interval = 500; //ms
            updater.Tick += Update;
            updater.Start();
        }

        private async void SetupSteamCmd()
        {
            steamCmd = new SteamCmd(userSettings.userSettingsDict["steamCmdInstallDir"]);

            if(!steamCmd.IsSteamCmdInstalled())
            {
                ProgressBarForm progress = new ProgressBarForm("Install Progress");
                progress.Show(this);
                progress.Center();
                SetControlEnable(false);

                await steamCmd.InstallSteamCmd(progress);

                progress.Close();
                SetControlEnable(true);
            }
        }

        private void SetControlEnable(bool value)
        {
            foreach(Control c in Controls)
            {
                c.Enabled = value;
            }
        }

        private void SetupUserAppData()
        {
            Directory.CreateDirectory(APPLICATION_USER_DATA_PATH);

            // Copy template files to new dir
            string[] templateNames = new string[] { KNOWN_SERVERS_FILENAME, USER_SETTINGS_FILENAME };
            foreach(string template in templateNames)
            {
                string sourceDir = APPLICATION_DATA_PATH + template;
                string targetDir = APPLICATION_USER_DATA_PATH + template;
                File.Copy(sourceDir, targetDir, true);
            }
        }

        private void WriteInitialData(Dictionary<string, FileStream> fileDict)
        {
            FileStream userSettingsFS = fileDict[USER_SETTINGS_FILENAME];
            FileStream serverListFS = fileDict[KNOWN_SERVERS_FILENAME];

            using (var writer = new StreamWriter(serverListFS))
            {
                writer.Write("ServerName,ServerPath,ServerPort,ServerLaunchArgs");
                writer.WriteLine();
                writer.Flush();
            }

            using (var writer = new StreamWriter(userSettingsFS))
            {
                writer.Write("completedSetup,false\r\nsteamInstallDir,\r\ndefaultServerDir,");
                writer.WriteLine();
                writer.Flush();
            }
        }

        private void TryWriteDefaultSettingsToServer(string serverPath)
        {
            bool hasSettings = false;

            if(!Directory.Exists(serverPath + PAL_SERVER_CONFIG_PATH))
            {
                // server doesn't have this dir, it hasn't been run yet. Can't import default settings;
                return;
            }

            using (FileStream fs = File.OpenRead(serverPath + PAL_SERVER_CONFIG_PATH))
            using (StreamReader reader = new StreamReader(fs))
            {
                string firstLineComment = reader.ReadLine();
                if (firstLineComment != null && firstLineComment[0] == PAL_CONFIG_COMMENT_CHAR)
                {
                    hasSettings = true;
                }
            }

            if(!hasSettings)
            {
                // Copy default settings file into server file
                File.Copy(serverPath + PAL_DEFAULT_CONFIG_PATH, serverPath + PAL_SERVER_CONFIG_PATH, true);
            }
        }

        private void HandleInitialSetup()
        {
            SettingsForm setupForm = new SettingsForm(userSettings);

            DialogResult result = setupForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                userSettings.userSettingsDict["completedSetup"] = "true";
                userSettings.userSettingsDict["steamInstallDir"] = setupForm.steamInstallPath;
                userSettings.userSettingsDict["defaultServerDir"] = setupForm.defaultServerInstallPath;
                userSettings.userSettingsDict["steamCmdInstallDir"] = setupForm.steamCmdInstallPath;

                userSettings.WriteUserSettings(APPLICATION_USER_DATA_PATH + USER_SETTINGS_FILENAME);
            }
            else
            {
                if (Application.MessageLoop)
                {
                    Application.Exit();
                }
                else
                {
                    Environment.Exit(1);
                }
            }
        }

        private Dictionary<string, int> GetServerNamesAndPIDs(Process[] processes)
        {
            Dictionary<string, int> namesToPID = new Dictionary<string, int>();

            foreach(Process process in processes)
            {
                if(process.HasExited)
                {
                    // When stopping a process, it will still be returned by Process.GetProcessesByName, but the MainModule will be null
                    continue;
                }

                string fullPath = process.MainModule.FileName;

                if(fullPath.Contains(PAL_SERVER_DIRECTORY_SUBSTRING)) // this substring is unique to the generated directory name
                {
                    // Split the path into 2 strings at the indicated substring
                    string[] pathSplitChars = { PAL_SERVER_DIRECTORY_SUBSTRING };
                    string[] splitPath = fullPath.Split(pathSplitChars, StringSplitOptions.None);

                    // Split the result of the above into all the subdirectories. The first element will be the name of the server, as generated
                    // by the tool when the server was created.
                    string[] nameSplitChars = { "\\" };
                    string[] nameSplitPath = splitPath[1].Split(nameSplitChars, StringSplitOptions.None);

                    namesToPID.Add(nameSplitPath[0], process.Id);
                }
            }

            return namesToPID;
        }

        private void LoadRunningServers()
        {
            Process[] serverProcs = Process.GetProcessesByName(SERVER_PROCESS_NAME);
            Dictionary<string, int> serverProcNames = GetServerNamesAndPIDs(serverProcs);

            for (int idx = 0; idx < knownServers.Count; idx++)
            {
                int serverProcessID = -1;
                if(serverProcNames.TryGetValue(knownServers[idx].ServerName, out serverProcessID))
                {
                    knownServers[idx].isRunning = true;
                    knownServers[idx].ProcessID = serverProcessID;
                }
            }
        }

        private void UpdateServerStatus()
        {
            LoadRunningServers();

            DataTable data = (DataTable)dataGridView1.DataSource;

            for(int idx = 0; idx < knownServers.Count; idx++) 
            {
                string value = knownServers[idx].isRunning ? "Running" : "Stopped";

                if (idx < data.Rows.Count)
                {
                    data.Rows[idx]["Server Status"] = value;
                }
            }
        }

        private void CheckAddServerThread()
        {
            if (addServerThread != null)
            {
                if (!addServerThread.IsAlive)
                {
                    ThreadedAddComplete();
                }
            }

            addServerBtn.Enabled = !isAdding; // don't allow concurrent adds, this will cause file exceptions since we copy from the same location
        }

        private void CheckImportServerThread()
        {
            if (importServerThread != null)
            {
                if (!importServerThread.IsAlive)
                {
                    ThreadedImportComplete();
                }
            }
        }

        private void CheckServerHasRunOnce(KnownServer server)
        {
            // if server doesn't have this dir, it hasn't been run yet.
            if (File.Exists(GetFullServerPath(server) + PAL_SERVER_CONFIG_PATH))
            {
                for (int idx = 0; idx < knownServers.Count; idx++)
                {
                    if (knownServers[idx] == server)
                    {
                        knownServers[idx].hasRunOnce = true;
                        break;
                    }
                }
            }
        }


        private void LoadCSVOnDataGridView(string fileName)
        {
            ServerDataTable reader = new ServerDataTable(fileName, true);

            if (reader.csvRead.Columns.Count > 0)
            {
                dataGridView1.DataSource = reader.csvRead;
                knownServers = reader.servers;

                foreach(KnownServer server in knownServers)
                {
                    CheckServerHasRunOnce(server);
                }
            }
            else
            {
                throw new Exception(string.Format("Could not load existing server CSV {0}", fileName));
            }
        }

        private void UpdateAndRefreshCSV()
        {
            ServerDataTable.WriteAllServersToCSV(knownServers, KNOWN_SERVER_PATH);

            LoadCSVOnDataGridView(KNOWN_SERVER_PATH);

            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        private void UpdateAndRefreshCSV(KnownServer newServer)
        {
            ServerDataTable.WriteServerToCSV(newServer, KNOWN_SERVER_PATH);

            LoadCSVOnDataGridView(KNOWN_SERVER_PATH);

            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        private void StartNewServer(KnownServer server)
        {
            if (!server.isRunning)
            {
                string portStr = string.Format("port={0} ", server.ServerPort);

                Process process = new Process();
                process.StartInfo.FileName = GetFullServerPath(server) + SERVER_EXE_NAME;
                process.StartInfo.Arguments = portStr + server.ServerLaunchArgs;
                process.StartInfo.WorkingDirectory = GetFullServerPath(server);

                process.Start();
            }
        }

        private KnownServer GetSelectedServer()
        {
            KnownServer server = null;

            int selectedRowIdx = dataGridView1.SelectedRows[0].Index;
            if (selectedRowIdx >= 0 && selectedRowIdx < knownServers.Count)
            {
                server = knownServers[selectedRowIdx];
            }

            return server;
        }

        private void startServerBtn_Click(object sender, EventArgs e)
        {
            KnownServer server = GetSelectedServer();

            if(server != null)
            {
                StartNewServer(server);
            }
        }

        private void stopServerBtn_Click(object sender, EventArgs e)
        {
            KnownServer server = GetSelectedServer();

            if (server != null)
            {
                if (server.isRunning)
                {
                    Process proc = Process.GetProcessById(server.ProcessID);
                    if (proc != null)
                    {
                        proc.Kill();
                    }

                    server.isRunning = false;
                    server.ProcessID = -1;
                }
            }
        }

        private bool AreDirectoriesAtSameLevel(string dir1, string dir2)
        {
            DirectoryInfo info1 = new DirectoryInfo(dir1);
            DirectoryInfo info2 = new DirectoryInfo(dir2);
            return info1.Parent.FullName == info2.Parent.FullName;
        }

        private bool CopyDirectoryRecursive(string sourceDir, string targetDir)
        {
            // If we're trying to copy but targetDir is already in sourceDir, just rename.
            // e.g. sourceDir = C:\PalworldServers\Server1 and targetDir = C:\PalworldServers
            if (AreDirectoriesAtSameLevel(targetDir, sourceDir))
            {
                Directory.Move(sourceDir, targetDir);
                return false;
            }

            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourceDir, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourceDir, targetDir));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories))
            {
                try
                {
                    File.Copy(newPath, newPath.Replace(sourceDir, targetDir), true);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
            }

            return true;
        }

        private void ThreadedAdd(string serverPath, string steamInstall)
        {
            lock(addServerComplete)
            {
                CopyDirectoryRecursive(steamInstall + DEFAULT_PAL_SERVER_DIR_NAME, serverPath);
            }
        }

        private void ThreadedAddComplete()
        {
            lock (addServerComplete)
            {
                UpdateAndRefreshCSV(currentAddingServer);

                CheckServerHasRunOnce(currentAddingServer);

                currentAddingServer = null;

                addServerThread = null;
                isAdding = false;
            }
        }

        private void addServerBtn_Click(object sender, EventArgs e)
        {
            AddServerForm.AddServerFormOptions options = new AddServerForm.AddServerFormOptions();
            options.defaultDir = userSettings.userSettingsDict["defaultServerDir"];
            options.isEditMenu = false;

            AddServerForm addServerForm = new AddServerForm(options);
            if (addServerForm.ShowDialog(this) == DialogResult.OK)
            {
                KnownServer newServer = new KnownServer();
                newServer.ServerName = addServerForm.newServerName;
                newServer.ServerPort = addServerForm.newServerPort;
                newServer.ServerPath = addServerForm.newServerPath;
                newServer.ServerLaunchArgs = addServerForm.newServerArgs;

                knownServers.Add(newServer);

                currentAddingServer = newServer;
                string fullServerPath = GetFullServerPath(newServer);
                string steamInstallDir = userSettings.userSettingsDict["steamInstallDir"];

                isAdding = true;

                addServerThread = new Thread(() =>
                {
                    ThreadedAdd(fullServerPath, steamInstallDir);
                });

                addServerThread.Start();
            }
        }

        private void removeServerBtn_Click(object sender, EventArgs e)
        {
            KnownServer server = GetSelectedServer();

            if (server != null)
            {
                string promptText = string.Format("Are you sure you want to remove {0}?", server.ServerName);

                ConfirmationPrompt confirmPrompt = new ConfirmationPrompt(promptText);
                confirmPrompt.Text = "Remove Server";

                DialogResult result = confirmPrompt.ShowDialog(this);
                if(result == DialogResult.OK)
                {
                    string serverPath = GetFullServerPath(server);

                    if(Directory.Exists(serverPath))
                    {
                        Directory.Delete(serverPath, true);
                    }

                    knownServers.Remove(server);

                    UpdateAndRefreshCSV();
                }
            }
        }

        private void editServerBtn_Click(object sender, EventArgs e)
        {
            AddServerForm.AddServerFormOptions options = new AddServerForm.AddServerFormOptions();
            options.defaultDir = "";
            options.isEditMenu = true;

            int selectedRowIdx = dataGridView1.SelectedRows[0].Index;
            if (selectedRowIdx >= 0 && selectedRowIdx < knownServers.Count)
            {
                options.editData = knownServers[selectedRowIdx];

                AddServerForm addServerForm = new AddServerForm(options);
                if (addServerForm.ShowDialog(this) == DialogResult.OK)
                {
                    KnownServer newServer = new KnownServer();
                    newServer.ServerName = addServerForm.newServerName;
                    newServer.ServerPort = addServerForm.newServerPort;
                    newServer.ServerPath = addServerForm.newServerPath;
                    newServer.ServerLaunchArgs = addServerForm.newServerArgs;

                    knownServers[selectedRowIdx] = newServer;

                    UpdateAndRefreshCSV();
                }
            }
        }

        private void editSettingsOption_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(userSettings);

            DialogResult result = settingsForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                userSettings.userSettingsDict["completedSetup"] = "true";
                userSettings.userSettingsDict["steamInstallDir"] = settingsForm.steamInstallPath;
                userSettings.userSettingsDict["defaultServerDir"] = settingsForm.defaultServerInstallPath;
                userSettings.userSettingsDict["steamCmdInstallDir"] = settingsForm.steamCmdInstallPath;

                userSettings.WriteUserSettings(APPLICATION_USER_DATA_PATH + USER_SETTINGS_FILENAME);
            }
        }

        private void ThreadedImport(string existingPathToMigrate, string fullServerPath)
        {
            lock(importServerComplete)
            {
                if (CopyDirectoryRecursive(existingPathToMigrate, fullServerPath))
                {
                    Directory.Delete(existingPathToMigrate, true);
                }

                TryWriteDefaultSettingsToServer(fullServerPath);
            }
        }

        private void ThreadedImportComplete()
        {
            lock (importServerComplete)
            {
                UpdateAndRefreshCSV(currentImportingServer);

                CheckServerHasRunOnce(currentImportingServer);

                currentImportingServer = null;

                importServerThread = null;
            }
        }

        private void importExistingServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportServerForm importServerForm = new ImportServerForm(userSettings.userSettingsDict["defaultServerDir"]);
            DialogResult result = importServerForm.ShowDialog(this);

            if(result== DialogResult.OK) 
            {
                string existingPathToMigrate = importServerForm.existingServerPath;

                KnownServer newServer = new KnownServer();
                newServer.ServerName = importServerForm.newServerName;
                newServer.ServerPort = importServerForm.newServerPort;
                newServer.ServerPath = importServerForm.newServerPath;
                newServer.ServerLaunchArgs = importServerForm.newServerArgs;

                knownServers.Add(newServer);
                string fullServerPath = GetFullServerPath(newServer);

                currentImportingServer = newServer;
                importServerThread = new Thread(() =>
                {
                    ThreadedImport(existingPathToMigrate, fullServerPath);
                });

                importServerThread.Start();
            }
        }

        private void dataGridView1_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            dataGridView1.ClearSelection();

            int rowSelected = e.RowIndex;
            if(rowSelected != -1 ) 
            {
                dataGridView1.Rows[rowSelected].Selected = true;
            }

            e.ContextMenuStrip = serverContextMenu;

            // Don't let server configs be edited while the server is running! Bad!!!
            KnownServer server = GetSelectedServer();
            if(server != null && !server.isRunning && server.hasRunOnce)
            {
                editGameConfigToolStripMenuItem.Enabled = true;
                openConfigToolStripMenuItem.Enabled = true;
            }
            else
            {
                editGameConfigToolStripMenuItem.Enabled = false;
                openConfigToolStripMenuItem.Enabled = false;
            }
        }

        private void contextMenuStart_Click(object sender, EventArgs e)
        {
            startServerBtn_Click(sender, e);
        }

        private void contextMenuStop_Click(object sender, EventArgs e)
        {
            stopServerBtn_Click(sender, e);
        }

        private void contextMenuEdit_Click(object sender, EventArgs e)
        {
            editServerBtn_Click(sender, e);
        }

        private void contextMenuRemove_Click(object sender, EventArgs e)
        {
            removeServerBtn_Click(sender, e);
        }

        private void helpToolStrip_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://github.com/cameronSchneider/PalworldServerManager");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void versionToolStrip_Click(object sender, EventArgs e)
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version; 
            versionTextBox.Text = string.Format("Version: {0}", version.ToString());
        }

        private void openFileLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KnownServer selectedServer = GetSelectedServer();

            string serverPath = GetFullServerPath(selectedServer);
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = serverPath;
            process.Start();
        }

        private void openConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KnownServer selectedServer = GetSelectedServer();

            string serverPath = GetFullServerPath(selectedServer);
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = serverPath + PAL_SERVER_CONFIG_PATH;
            process.Start();
        }

        private void openDefaultSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KnownServer selectedServer = GetSelectedServer();

            string serverPath = GetFullServerPath(selectedServer);
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = serverPath + PAL_DEFAULT_CONFIG_PATH;
            process.Start();
        }
        
        private void editGameConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KnownServer selectedServer = GetSelectedServer();
            EditGameSettingsForm editForm = new EditGameSettingsForm(GetFullServerPath(selectedServer) + PAL_SERVER_CONFIG_PATH);

            editForm.ShowDialog(this);
        }
    }
}
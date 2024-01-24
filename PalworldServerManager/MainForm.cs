using ApplicationDataUtilities;
using ProcessPortUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PalworldServerManager
{
    public partial class MainForm : Form
    {
        public static string APPLICATION_DATA_PATH = Application.StartupPath + "\\Data\\";
        public static string KNOWN_SERVERS_FILENAME = "known_servers.csv";
        public static string USER_SETTINGS_FILENAME = "usersettings.csv";
        public static string SERVER_PROCESS_NAME = "PalServer-Win64-Test-Cmd";
        public static string SERVER_EXE_NAME = "\\PalServer.exe";
        public static string DEFAULT_PAL_SERVER_DIR_NAME = "\\PalServer";

        public static string PAL_SERVER_DIRECTORY_SUBSTRING = "PalServer - ";

        public static string KNOWN_SERVER_PATH = APPLICATION_DATA_PATH + KNOWN_SERVERS_FILENAME;

        public UserSettings userSettings = new UserSettings();
        public List<KnownServerRow> knownServers;

        private static string GetFullServerPath(KnownServerRow server)
        {
            return server.ServerPath + DEFAULT_PAL_SERVER_DIR_NAME + " - " + server.ServerName;
        }

        private void Update(object sender, EventArgs e)
        {
            UpdateServerStatus();
        }

        public MainForm()
        {
            InitializeComponent();

            userSettings.ReadUserSettings(APPLICATION_DATA_PATH + USER_SETTINGS_FILENAME);

            LoadCSVOnDataGridView(KNOWN_SERVER_PATH);
            dataGridView1.CellContextMenuStripNeeded += dataGridView1_CellContextMenuStripNeeded;

            LoadRunningServers();

            if (userSettings.userSettingsDict["completedSetup"] == "false")
            {
                SettingsForm setupForm = new SettingsForm(userSettings);

                DialogResult result = setupForm.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    userSettings.userSettingsDict["completedSetup"] = "true";
                    userSettings.userSettingsDict["steamInstallDir"] = setupForm.steamInstallPath;
                    userSettings.userSettingsDict["defaultServerDir"] = setupForm.defaultServerInstallPath;

                    userSettings.WriteUserSettings(APPLICATION_DATA_PATH + USER_SETTINGS_FILENAME);
                }
                else
                {
                    if(Application.MessageLoop)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        Environment.Exit(1);
                    }
                }
            }

            System.Windows.Forms.Timer updater = new System.Windows.Forms.Timer();
            updater.Interval = 1000; //ms
            updater.Tick += Update;
            updater.Start();
        }


        private KnownServerRow FindKnownServerByPort(int port)
        {
            foreach (KnownServerRow server in knownServers)
            {
                if (Convert.ToInt32(server.ServerPort) == port)
                {
                    return server;
                }
            }

            return null;
        }

        private Dictionary<string, int> GetServerNamesAndPIDs(List<Process> processes)
        {
            Dictionary<string, int> namesToPID = new Dictionary<string, int>();

            foreach(Process process in processes)
            {
                if(process.HasExited)
                {
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
            Dictionary<string, int> serverProcNames = GetServerNamesAndPIDs(serverProcs.ToList());

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
                data.Rows[idx]["Server Status"] = value;
            }
        }

        private void LoadCSVOnDataGridView(string fileName)
        {
            ServerDataTable reader = new ServerDataTable(fileName, true);

            if (reader.csvRead.Columns.Count > 0)
            {
                dataGridView1.DataSource = reader.csvRead;
                knownServers = reader.servers;
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

        private void UpdateAndRefreshCSV(KnownServerRow newServer)
        {
            ServerDataTable.WriteServerToCSV(newServer, KNOWN_SERVER_PATH);

            LoadCSVOnDataGridView(KNOWN_SERVER_PATH);

            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        private void StartNewServer(KnownServerRow server)
        {
            if (!server.isRunning)
            {
                // Start up this server using its registered path, port, and launch args
                string portStr = string.Format("port={0} ", server.ServerPort);

                Process process = new Process();
                process.StartInfo.FileName = GetFullServerPath(server) + SERVER_EXE_NAME;
                process.StartInfo.Arguments = portStr + server.ServerLaunchArgs;
                process.StartInfo.WorkingDirectory = GetFullServerPath(server);

                process.Start();
            }
        }

        private KnownServerRow GetSelectedServer()
        {
            KnownServerRow server = null;

            int selectedRowIdx = dataGridView1.SelectedRows[0].Index;
            if (selectedRowIdx >= 0 && selectedRowIdx < knownServers.Count)
            {
                server = knownServers[selectedRowIdx];
            }

            return server;
        }

        private void startServerBtn_Click(object sender, EventArgs e)
        {
            KnownServerRow server = GetSelectedServer();

            if(server != null)
            {
                StartNewServer(server);
            }
        }

        private void stopServerBtn_Click(object sender, EventArgs e)
        {
            KnownServerRow server = GetSelectedServer();

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

        private void CopyDirectoryRecursive(string sourceDir, string targetDir)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourceDir, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourceDir, targetDir));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourceDir, targetDir), true);
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
                KnownServerRow newServer = new KnownServerRow();
                newServer.ServerName = addServerForm.newServerName;
                newServer.ServerPort = addServerForm.newServerPort;
                newServer.ServerPath = addServerForm.newServerPath;
                newServer.ServerLaunchArgs = addServerForm.newServerArgs;

                knownServers.Add(newServer);

                CopyDirectoryRecursive(userSettings.userSettingsDict["steamInstallDir"] + DEFAULT_PAL_SERVER_DIR_NAME, GetFullServerPath(newServer));

                UpdateAndRefreshCSV(newServer);
            }
        }

        private void removeServerBtn_Click(object sender, EventArgs e)
        {
            KnownServerRow server = GetSelectedServer();

            if (server != null)
            {
                string promptText = string.Format("Are you sure you want to remove {0}?", server.ServerName);

                ConfirmationPrompt confirmPrompt = new ConfirmationPrompt(promptText);
                confirmPrompt.Text = "Remove Server";

                DialogResult result = confirmPrompt.ShowDialog(this);
                if(result == DialogResult.OK)
                {
                    Directory.Delete(GetFullServerPath(server), true);

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
                    KnownServerRow newServer = new KnownServerRow();
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

                userSettings.WriteUserSettings(APPLICATION_DATA_PATH + USER_SETTINGS_FILENAME);
            }
        }

        private void importExistingServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportServerForm importServerForm = new ImportServerForm(userSettings.userSettingsDict["defaultServerDir"]);
            DialogResult result = importServerForm.ShowDialog(this);

            if(result== DialogResult.OK) 
            {
                string existingPathToMigrate = importServerForm.existingServerPath;

                KnownServerRow newServer = new KnownServerRow();
                newServer.ServerName = importServerForm.newServerName;
                newServer.ServerPort = importServerForm.newServerPort;
                newServer.ServerPath = importServerForm.newServerPath;
                newServer.ServerLaunchArgs = importServerForm.newServerArgs;

                knownServers.Add(newServer);

                CopyDirectoryRecursive(existingPathToMigrate, GetFullServerPath(newServer));
                Directory.Delete(existingPathToMigrate, true);

                UpdateAndRefreshCSV(newServer);
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

            e.ContextMenuStrip = contextMenuStrip1;
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
    }
}
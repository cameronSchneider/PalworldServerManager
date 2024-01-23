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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PalworldServerManager
{
    public partial class MainForm : Form
    {
        public static string APPLICATION_DATA_PATH = Application.StartupPath + "\\Data\\";
        public static string EXISTING_SERVERS_FILENAME = "known_servers.csv";
        public static string USER_SETTINGS_FILENAME = "usersettings.csv";
        public static string SERVER_PROCESS_NAME = "PalServer-Win64-Test-Cmd";
        public static string SERVER_EXE_NAME = "PalServer.exe";

        public UserSettings userSettings = new UserSettings();
        public List<KnownServerRow> knownServers;

        public MainForm()
        {
            InitializeComponent();

            userSettings.ReadUserSettings(APPLICATION_DATA_PATH + USER_SETTINGS_FILENAME);

            string existingServerDataFileName = APPLICATION_DATA_PATH + EXISTING_SERVERS_FILENAME;
            LoadCSVOnDataGridView(existingServerDataFileName);


            LoadRunningServers();

            if (userSettings.userSettingsDict["completedSetup"] == "false")
            {
                FirstTimeSetupForm setupForm = new FirstTimeSetupForm(userSettings);

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


        private void LoadRunningServers()
        {
            // Try to optimize the port list search
            List<int> searchPorts = new List<int>();
            foreach (KnownServerRow existing in knownServers)
            {
                searchPorts.Add(Convert.ToInt32(existing.ServerPort));
            }

            List<ProcessPort> serverProcs = ProcessPorts.FindPortsInMap(searchPorts, ProcessPorts.Protocol.UDP);
            foreach (ProcessPort processPort in serverProcs)
            {
                KnownServerRow existing = FindKnownServerByPort(processPort.PortNumber);

                if (existing != null && processPort.ProcessId > 0 && processPort.ProcessName == SERVER_PROCESS_NAME)
                {
                    existing.isRunning = true;
                    existing.ProcessID = processPort.ProcessId;
                }
            }

        }


        private void LoadRunningServerByPort(int portNumber)
        {
            ProcessPort serverProc = ProcessPorts.FindPortInMap(portNumber, ProcessPorts.Protocol.UDP);

            KnownServerRow serverData = FindKnownServerByPort(portNumber);

            if (serverData != null && serverProc != null && serverProc.ProcessName == SERVER_PROCESS_NAME)
            {
                serverData.isRunning = true;
                serverData.ProcessID = serverProc.ProcessId;
            }
        }


        private void LoadCSVOnDataGridView(string fileName)
        {
            try
            {
                ServerDataTable reader = new ServerDataTable(fileName, true);

                try
                {
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
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void StartNewServer(KnownServerRow server)
        {
            if (!server.isRunning)
            {
                // Start up this server using its registered path, port, and launch args
                string portStr = string.Format("port={0} ", server.ServerPort);

                Process process = new Process();
                process.StartInfo.FileName = server.ServerPath + "\\" + SERVER_EXE_NAME;
                process.StartInfo.Arguments = portStr + server.ServerLaunchArgs;
                process.StartInfo.WorkingDirectory = server.ServerPath;

                if (process.Start())
                {
                    Thread.Sleep(3000);

                    // The above starts the bootstrapper, which in-turn starts the server app. Reload the running servers to get that new data
                    LoadRunningServerByPort(Convert.ToInt32(server.ServerPort));
                }
            }
        }

        private void startServerBtn_Click(object sender, EventArgs e)
        {
            int selectedRowIdx = dataGridView1.SelectedRows[0].Index;

            if (selectedRowIdx >= 0 && selectedRowIdx < knownServers.Count)
            {
                KnownServerRow server = knownServers[selectedRowIdx];

                StartNewServer(server);
            }
        }

        private void stopServerBtn_Click(object sender, EventArgs e)
        {
            int selectedRowIdx = dataGridView1.SelectedRows[0].Index;

            if (selectedRowIdx >= 0 && selectedRowIdx < knownServers.Count)
            {
                KnownServerRow server = knownServers[selectedRowIdx];

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

        public void AddServerFinished(KnownServerRow newServerData)
        {
            string existingServerDataFileName = APPLICATION_DATA_PATH + EXISTING_SERVERS_FILENAME;
            knownServers.Add(newServerData);

            ServerDataTable.WriteServerToCSV(newServerData, existingServerDataFileName);
            LoadCSVOnDataGridView(existingServerDataFileName);

            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        private void addServerBtn_Click(object sender, EventArgs e)
        {
            var addServerForm = new AddServerForm();
            addServerForm.ShowDialog(this);
        }

        private void removeServerBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
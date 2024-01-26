using System.Linq;
using System.Data;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ApplicationDataUtilities
{
    public class KnownServer
    {
        [Index(0)]
        public string ServerName { get; set; }
        [Index(1)]
        public string ServerPath { get; set; }
        [Index(2)]
        public string ServerPort { get; set; }
        [Index(3)]
        public string ServerLaunchArgs { get; set; }

        public bool isRunning = false;
        public int ProcessID = -1;
        public bool hasRunOnce = false;

        public object[] GetData()
        {
            return new object[] { ServerName, ServerPath, ServerPort, ServerLaunchArgs };
        }
    }

    public class CSVDataHelper
    {
        public static bool DoesDataDirectoryExist(string path)
        {
            return Directory.Exists(path);
        }

        public static Dictionary<string, FileStream> CreateInitialDataFiles(string path, string[] fileNames) 
        {
            Directory.CreateDirectory(path);

            Dictionary<string, FileStream> fileDict = new Dictionary<string, FileStream>();
            foreach(string file in fileNames)
            {
                FileStream fs = File.Create(path + file);
                fileDict.Add(file, fs);
            }

            return fileDict;
        }
    }

    public class ServerDataTable
    {
        public DataTable csvRead;
        public List<KnownServer> servers = new List<KnownServer>();

        public ServerDataTable(string fileName, bool firstRowContainsFieldNames = true)
        {
            csvRead = GenerateDataTable(fileName, firstRowContainsFieldNames, out servers);
        }

        private static DataTable GenerateDataTable(string fileName, bool firstRowContainsFieldNames, out List<KnownServer> outServers)
        {
            DataTable result = new DataTable();
            outServers = new List<KnownServer>();

            if (fileName == "")
            {
                throw new System.Exception("Filename empty");
            }

            if(!File.Exists(fileName)) 
            {
                return result;
            }

            using(StreamReader reader = new StreamReader(fileName)) 
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    if (firstRowContainsFieldNames) 
                    {
                        csv.Read();
                        csv.ReadHeader();
                        string[] headerRow = csv.HeaderRecord;

                        foreach (string header in headerRow)
                        {
                            result.Columns.Add(header);
                        }
                    }

                    while(csv.Read())
                    {
                        KnownServer record = csv.GetRecord<KnownServer>();
                        result.Rows.Add(record.GetData());
                        outServers.Add(record);
                    }
                }
            }

            result.Columns.Add("Server Status");

            return result;
        }

        public static void WriteServerToCSV(KnownServer server, string fileName)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = false,
            };

            using (var stream = File.Open(fileName, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecord(server);
                csv.Flush();

                // CSVHelper secretly relies on having a newline at EOF but doesn't do it alone :)
                writer.WriteLine();
                writer.Flush();
            }
        }

        public static void WriteAllServersToCSV(List<KnownServer> servers, string fileName)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = true,
            };

            using (var writer = new StreamWriter(fileName))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(servers);
                csv.Flush();
            }
        }
    }


    public class UserSettingEntry
    {
        [Index(0)]
        public string key { get; set; }
        [Index(1)]
        public string value { get; set; }
    }


    public class UserSettings
    {
        public Dictionary<string, string> userSettingsDict = new Dictionary<string, string>();

        public void ReadUserSettings(string fileName)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            using (StreamReader reader = new StreamReader(fileName))
            {
                using (var csv = new CsvReader(reader, config))
                {
                    while (csv.Read())
                    {
                        UserSettingEntry record = csv.GetRecord<UserSettingEntry>();
                        userSettingsDict.Add(record.key, record.value);
                    }
                }
            }
        }

        public void WriteUserSettings(string fileName) 
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = false,
            };

            File.Create(fileName).Close();

            using (var writer = new StreamWriter(fileName))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(userSettingsDict);
                csv.Flush();

                // CSVHelper secretly relies on having a newline at EOF but doesn't do it alone :)
                writer.WriteLine();
                writer.Flush();
            }
        }
    }
}
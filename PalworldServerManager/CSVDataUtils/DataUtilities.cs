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
    public class KnownServerRow
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

        public object[] GetData()
        {
            return new object[] { ServerName, ServerPath, ServerPort, ServerLaunchArgs };
        }
    }

    public class ServerDataTable
    {
        public DataTable csvRead;
        public List<KnownServerRow> servers = new List<KnownServerRow>();

        public ServerDataTable(string fileName, bool firstRowContainsFieldNames = true)
        {
            csvRead = GenerateDataTable(fileName, firstRowContainsFieldNames, out servers);
        }

        private static DataTable GenerateDataTable(string fileName, bool firstRowContainsFieldNames, out List<KnownServerRow> outServers)
        {
            DataTable result = new DataTable();
            outServers = new List<KnownServerRow>();

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
                        KnownServerRow record = csv.GetRecord<KnownServerRow>();
                        result.Rows.Add(record.GetData());
                        outServers.Add(record);
                    }
                }
            }

            result.Columns.Add("Server Status");

            return result;
        }

        public static void WriteServerToCSV(KnownServerRow server, string fileName)
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

        public static void WriteAllServersToCSV(List<KnownServerRow> servers, string fileName)
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration;

namespace PalworldServerManager
{
    public partial class EditGameSettingsForm : Form
    {
        private string settingsPath = "";
        public enum SettingValueType
        {
            Boolean,
            DeathPenalty,
            Difficulty,
            Int,
            Float,
            String
        };

        public struct GameSettingValue
        {
            public string Value;
            public SettingValueType Type;
        }

        public class GameSettingOptionEntry
        {
            [Index(0)]
            public string SettingName { get; set; }

            [Index(1)]
            public string Options { get; set; }
        }

        public class GameSettingDescriptionEntry
        {
            [Index(0)]
            public string SettingName { get; set; }

            [Index(1)]
            public string Description { get; set; }
        }


        private Dictionary<string, GameSettingValue> gameSettings = new Dictionary<string, GameSettingValue>();
        private Dictionary<string, Tuple<GameSettingValue, GameSettingValue>> changedSettings = new Dictionary<string, Tuple<GameSettingValue, GameSettingValue>>();

        private Dictionary<string, string> gameSettingOptions = new Dictionary<string, string>();
        private Dictionary<string, string> gameSettingDescriptions = new Dictionary<string, string>();

        public EditGameSettingsForm(string settingsFilePath)
        {
            InitializeComponent();

            if(File.Exists(settingsFilePath))
            {
                settingsPath = settingsFilePath;

                LoadSettingOptions();
                LoadSettingDescriptions();

                ParseGameSettingsFile();
                SetUpDataViewGrid();
            }
            else
            {
                Close();
                DialogResult = DialogResult.Abort;
            }
        }

        private void LoadSettingOptions()
        {
            using (StreamReader reader = new StreamReader(ProgramConstants.APPLICATION_DATA_PATH + ProgramConstants.PAL_GAME_SETTING_OPTIONS_FILE))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while(csv.Read())
                    {
                        GameSettingOptionEntry optionEntry = csv.GetRecord<GameSettingOptionEntry>();
                        gameSettingOptions.Add(optionEntry.SettingName, optionEntry.Options);
                    }
                }
            }
        }

        private void LoadSettingDescriptions()
        {
            using (StreamReader reader = new StreamReader(ProgramConstants.APPLICATION_DATA_PATH + ProgramConstants.PAL_GAME_SETTING_DESCRIPTIONS_FILE))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        GameSettingDescriptionEntry optionEntry = csv.GetRecord<GameSettingDescriptionEntry>();
                        gameSettingDescriptions.Add(optionEntry.SettingName, optionEntry.Description);
                    }
                }
            }
        }

        private void ParseGameSettingsFile()
        {
            string settingsString = "";
            using (FileStream settingsFile = File.Open(settingsPath, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(settingsFile)) 
            {
                // Skip comment lines until we hit the header
                string curLine = reader.ReadLine();
                while (curLine[0] == ProgramConstants.PAL_CONFIG_COMMENT_CHAR)
                {
                    curLine = reader.ReadLine();
                    continue;
                }

                settingsString = reader.ReadToEnd().TrimStart(ProgramConstants.PAL_SETTINGS_OPTION_STR.ToCharArray()); // Trim OptionsSettings=(, we only want raw settings
            }

            string[] individualSettings = settingsString.Split(',');

            // The last setting will include a closing paren, and newlines due to the nature of the default file. Trim that.
            string lastSetting = individualSettings[individualSettings.Length - 1];
            lastSetting = lastSetting.Replace("\r\n", string.Empty).TrimEnd(')');

            individualSettings[individualSettings.Length - 1] = lastSetting;

            foreach (string setting in individualSettings) 
            {
                string[] keyValue = setting.Split('=');

                if (keyValue[1][0] == '"')
                {
                    // trim start and end quotes on strings, re-add them later during serialization
                    string value = keyValue[1].TrimStart('"').TrimEnd('"');
                    keyValue[1] = value;
                }

                GameSettingValue newVal = new GameSettingValue();
                newVal.Value = keyValue[1];
                newVal.Type = GetSettingValueTypeFromString(keyValue[0], keyValue[1]);

                gameSettings[keyValue[0]] = newVal;
            }
        }

        public static DataGridViewCell CreateSettingCell(string setting)
        {
            return new DataGridViewTextBoxCell()
            {
                ValueType = typeof(string),
                Value = setting,
            };
        }

        private string[] GetDeathPenaltyOptions()
        {
            string[] options = gameSettingOptions["DeathPenalty"].Split('|');
            return options;
        }

        private string[] GetDifficultyOptions()
        {
            string[] options = gameSettingOptions["Difficulty"].Split('|');
            return options;
        }

        private DataGridViewCell CreateValueCell(SettingValueType type, string value) 
        {
            DataGridViewCell cell;

            switch(type)
            {
                case SettingValueType.Boolean:
                    cell = new DataGridViewCheckBoxCell()
                    { Value = value, ValueType = typeof(bool) };
                    break;
                case SettingValueType.DeathPenalty:
                    var selectablePenalties = GetDeathPenaltyOptions();
                    cell = new DataGridViewComboBoxCell()
                    {
                        DataSource = new BindingList<string>(selectablePenalties),
                        Value = value,
                    };
                    break;
                case SettingValueType.Difficulty:
                    var selectableDifficulties = GetDifficultyOptions();
                    cell = new DataGridViewComboBoxCell()
                    {
                        DataSource = new BindingList<string>(selectableDifficulties),
                        Value = value,
                    };
                    break;
                case SettingValueType.Int:
                case SettingValueType.Float:
                case SettingValueType.String:
                default:
                    cell = new DataGridViewTextBoxCell()
                    { Value = value, ValueType = typeof(string) };
                    break;
            }

            return cell;
        }

        private void AddRow(string setting, string value, SettingValueType type) 
        {
            DataGridViewRow row = new DataGridViewRow();
            row.Cells.Add(CreateSettingCell(setting));
            row.Cells.Add(CreateValueCell(type, value));

            row.Cells[0].ReadOnly = true;

            settingsDataGrid.Rows.Add(row);
        }

        private SettingValueType GetSettingValueTypeFromString(string setting, string value)
        {
            if (value.ToLower() == "false" || value.ToLower() == "true")
            {
                return SettingValueType.Boolean;
            }

            if(setting == "Difficulty")
            {
                return SettingValueType.Difficulty;
            }

            int intVal = 0;
            float floatVal = 0.0f;
            if (int.TryParse(value, out intVal))
            {
                return SettingValueType.Int;
            }
            else if (float.TryParse(value, out floatVal))
            {
                return SettingValueType.Float;
            }

            if (GetDeathPenaltyOptions().Contains(value))
            {
                return SettingValueType.DeathPenalty;
            }

            return SettingValueType.String;
        }

        private void SetUpDataViewGrid()
        {
            // Generate rows from Dictionary
            // https://stackoverflow.com/questions/18045248/adding-different-datagridview-cell-types-to-a-column

            settingsDataGrid.EditingControlShowing += HandleEditingControlShowing;
            settingsDataGrid.CellEndEdit += HandleCellEditComplete;
            settingsDataGrid.SelectionChanged += HandleRowSelectionChanged;
            settingsDataGrid.CellEnter += HandleCellSelectionChanged;

            settingsDataGrid.Columns.Add("Setting", "Setting");
            settingsDataGrid.Columns.Add("Value", "Value");

            foreach(KeyValuePair<string, GameSettingValue> kvp in gameSettings)
            {
                string setting = kvp.Key;

                AddRow(setting, kvp.Value.Value, kvp.Value.Type);
            }

            settingsDataGrid.Rows[0].Selected = true;
        }

        private void HandleGridKeyDown(object sender, KeyEventArgs e)
        {
            DataGridViewCell cell = settingsDataGrid.CurrentCell;
            var cellVal = cell.Value;

            int intVal = 0;
            float floatVal = 0.0f;
            if (int.TryParse(cellVal.ToString(), out intVal))
            {
                e.SuppressKeyPress = !IsValidIntKey(e.KeyCode);
            }
            else if (float.TryParse(cellVal.ToString(), out floatVal))
            {
                e.SuppressKeyPress = !IsValidFloatKey(e.KeyCode);
            }
            else
            {
                e.SuppressKeyPress = false;
            }
        }

        private bool IsValidIntKey(Keys code) 
        {
            return (code >= Keys.D0 && code <= Keys.D9 ||
               code >= Keys.NumPad0 && code <= Keys.NumPad9 ||
               code == Keys.Back ||
               code == Keys.Enter);
        }

        private bool IsValidFloatKey(Keys code)
        {
            return IsValidIntKey(code) || code == Keys.Decimal || code == Keys.OemPeriod;
        }

        private void HandleEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl tb)
            {
                tb.KeyDown += HandleGridKeyDown;
            }
        }

        private void HandleCellEditComplete(object sender, DataGridViewCellEventArgs e)
        {
            string settingName = settingsDataGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            string value = settingsDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            GameSettingValue oldVal = gameSettings[settingName];

            if(value == "")
            {
                settingsDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = oldVal.Value;
                return;
            }

            if (gameSettings.ContainsKey(settingName))
            {
                GameSettingValue newVal = new GameSettingValue();
                newVal.Value = value;
                newVal.Type = gameSettings[settingName].Type;

                Tuple<GameSettingValue, GameSettingValue> oldNewChange = new Tuple<GameSettingValue, GameSettingValue>(oldVal, newVal);
                changedSettings.Add(settingName, oldNewChange);

                gameSettings[settingName] = newVal;
            }
        }

        private void HandleRowSelectionChanged(object sender, EventArgs e)
        {
            if(settingsDataGrid.SelectedRows.Count > 0)
            {
                string selectedSetting = settingsDataGrid.SelectedRows[0].Cells[0].Value.ToString();

                if(gameSettingDescriptions.ContainsKey(selectedSetting))
                {
                    settingDescTxt.Text = gameSettingDescriptions[selectedSetting];
                }
            }
        }

        private void HandleCellSelectionChanged(object sender, EventArgs e)
        {
            if(settingsDataGrid.SelectedCells.Count > 0)
            {
                int selectedCellRow = settingsDataGrid.SelectedCells[0].RowIndex;
                settingsDataGrid.Rows[selectedCellRow].Selected = true;
            }   
        }

        private void completeBtn_Click(object sender, EventArgs e)
        {
            EditGameSettingsConfirmation confirm = new EditGameSettingsConfirmation(changedSettings);

            if(changedSettings.Count > 0 && confirm.ShowDialog(this) == DialogResult.OK) 
            {
                SerializeGameSettings();
            }

            Close();
            DialogResult = DialogResult.OK;
        }

        private void SerializeGameSettings()
        {
            // Settings are stored as one string.
            string settingsString = ProgramConstants.PAL_SETTINGS_OPTION_STR;

            foreach (KeyValuePair<string, GameSettingValue> kvp in gameSettings)
            {
                string individualSettingStr = kvp.Key + '=';
                switch (kvp.Value.Type)
                {
                    // These types require no formatting, just write as-is
                    case SettingValueType.Boolean:
                    case SettingValueType.DeathPenalty:
                    case SettingValueType.Int:
                        individualSettingStr += kvp.Value.Value;
                        break;
                    // Float requires precision of 6 decimal places, ensure that happens
                    case SettingValueType.Float:
                        float val = 0.0f;
                        float.TryParse(kvp.Value.Value, out val);
                        string floatStrFormatted = val.ToString("N6");
                        individualSettingStr += floatStrFormatted;
                        break;
                    // String requires quotes before and after the value
                    case SettingValueType.String:
                        string formatted = '"' + kvp.Value.Value + '"';
                        individualSettingStr += formatted;
                        break;
                }

                if(kvp.Key != gameSettings.Last().Key)
                {
                    settingsString += individualSettingStr + ',';
                }
                else
                {
                    // at the very end, close the OptionSettings=( parenthesis
                    settingsString += individualSettingStr + ')';
                }
            }

            File.Create(settingsPath).Close(); // Wipe the existing file for overwrite

            using (FileStream settingsFile = File.Open(settingsPath, FileMode.Open, FileAccess.ReadWrite))
            using (StreamWriter writer = new StreamWriter(settingsFile))
            {
                writer.WriteLine(ProgramConstants.PAL_SETTINGS_HEADER);
                writer.Write(settingsString);
            }
        }
    }
}

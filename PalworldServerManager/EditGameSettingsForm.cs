using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace PalworldServerManager
{
    public partial class EditGameSettingsForm : Form
    {
        private string settingsPath = "";

        struct GameSettingValue
        {
            public string Value;
            public SettingValueType Type;
        }

        private Dictionary<string, GameSettingValue> gameSettings = new Dictionary<string, GameSettingValue>();

        private enum SettingValueType
        {
            Boolean,
            DeathPenalty,
            Int,
            Float,
            String
        };

        public EditGameSettingsForm(string settingsFilePath)
        {
            InitializeComponent();

            if(File.Exists(settingsFilePath))
            {
                settingsPath = settingsFilePath;

                ParseGameSettingsFile();
                SetUpDataViewGrid();
                settingsDataGrid.EditingControlShowing += HandleEditingControlShowing;
                settingsDataGrid.CellEndEdit += HandleCellEditComplete;
            }
            else
            {
                Close();
                DialogResult = DialogResult.Abort;
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
                while (curLine[0] == MainForm.PAL_CONFIG_COMMENT_CHAR)
                {
                    curLine = reader.ReadLine();
                    continue;
                }

                settingsString = reader.ReadToEnd().TrimStart(MainForm.PAL_SETTINGS_OPTION_STR.ToCharArray()); // Trim OptionsSettings=(, we only want raw settings
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
                newVal.Type = GetSettingValueTypeFromString(keyValue[1]);

                gameSettings[keyValue[0]] = newVal;
            }
        }

        private DataGridViewCell CreateSettingCell(string setting)
        {
            return new DataGridViewTextBoxCell()
            {
                ValueType = typeof(string),
                Value = setting,
            };
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
                    var selectablePenalties = new string[] { "None", "Item", "ItemAndEquipment", "All" };
                    cell = new DataGridViewComboBoxCell()
                    {
                        DataSource = new BindingList<string>(selectablePenalties),
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

        private SettingValueType GetSettingValueTypeFromString(string value)
        {
            if (value.ToLower() == "false" || value.ToLower() == "true")
            {
                return SettingValueType.Boolean;
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

            if (value.ToLower() == "none" || 
                value.ToLower() == "item" ||
                value.ToLower() == "itemandequipment" ||
                value.ToLower() == "all")
            {
                return SettingValueType.DeathPenalty;
            }

            return SettingValueType.String;
        }

        private void SetUpDataViewGrid()
        {
            // Generate rows from Dictionary
            // https://stackoverflow.com/questions/18045248/adding-different-datagridview-cell-types-to-a-column

            settingsDataGrid.Columns.Add("Setting", "Setting");
            settingsDataGrid.Columns.Add("Value", "Value");

            foreach(KeyValuePair<string, GameSettingValue> kvp in gameSettings)
            {
                string setting = kvp.Key;

                AddRow(setting, kvp.Value.Value, kvp.Value.Type);
            }
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

            if(gameSettings.ContainsKey(settingName))
            {
                GameSettingValue newVal = new GameSettingValue();
                newVal.Value = value;
                newVal.Type = gameSettings[settingName].Type;

                gameSettings[settingName] = newVal;
            }
        }

        private void completeBtn_Click(object sender, EventArgs e)
        {
            ConfirmationPrompt confirm = new ConfirmationPrompt("Commit these setting changes? This will overwrite current settings.");

            if(confirm.ShowDialog(this) == DialogResult.OK) 
            {
                SerializeGameSettings();
                Close();
                DialogResult = DialogResult.OK;
            }
        }

        private void SerializeGameSettings()
        {
            // Settings are stored as one string.
            string settingsString = MainForm.PAL_SETTINGS_OPTION_STR;

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
                writer.WriteLine(MainForm.PAL_SETTINGS_HEADER);
                writer.Write(settingsString);
            }
        }
    }
}

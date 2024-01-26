using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PalworldServerManager.EditGameSettingsForm;

namespace PalworldServerManager
{
    public partial class EditGameSettingsConfirmation : Form
    {
        public EditGameSettingsConfirmation(Dictionary<string, Tuple<GameSettingValue, GameSettingValue>> changedSettings)
        {
            InitializeComponent();
            SetupDataGrid(changedSettings);
        }

        private DataGridViewCell CreateValueCell(SettingValueType type, string value)
        {
            DataGridViewCell cell;

            switch (type)
            {
                case SettingValueType.Boolean:
                    cell = new DataGridViewCheckBoxCell()
                    { Value = value, ValueType = typeof(bool) };
                    break;
                case SettingValueType.DeathPenalty:
                case SettingValueType.Difficulty:
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

        private void AddRow(string setting, Tuple<GameSettingValue, GameSettingValue> oldNewValueTuple)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.Cells.Add(CreateSettingCell(setting));
            row.Cells.Add(CreateValueCell(oldNewValueTuple.Item1.Type, oldNewValueTuple.Item1.Value));
            row.Cells.Add(CreateValueCell(oldNewValueTuple.Item2.Type, oldNewValueTuple.Item2.Value));

            changedSettingsDataGrid.Rows.Add(row);
        }

        private void SetupDataGrid(Dictionary<string, Tuple<GameSettingValue, GameSettingValue>> changedSettings)
        {
            changedSettingsDataGrid.Columns.Add("Setting", "Setting");
            changedSettingsDataGrid.Columns.Add("OldValue", "Old Value");
            changedSettingsDataGrid.Columns.Add("NewValue", "New Value");

            foreach (KeyValuePair<string, Tuple<GameSettingValue, GameSettingValue>> kvp in changedSettings)
            {
                string setting = kvp.Key;
                AddRow(setting, kvp.Value);
            }
        }
    }
}

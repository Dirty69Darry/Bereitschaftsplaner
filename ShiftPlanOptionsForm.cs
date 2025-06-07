using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Bereitschaftsplaner
{
    public partial class ShiftPlanOptionsForm : Form
    {
        private NumericUpDown nudYear;
        private NumericUpDown nudPeriodLength;
        private ComboBox cmbWeekday;
        private Button btnOk;
        private Button btnCancel;

        public int SelectedYear => (int)nudYear.Value;
        public int PeriodLengthDays => (int)nudPeriodLength.Value;
        public DayOfWeek FirstWeekday => (DayOfWeek)cmbWeekday.SelectedValue;

        public ShiftPlanOptionsForm(int initialYear, int initialPeriod)
        {
            var culture = new CultureInfo("de-DE");
            var days = Enum.GetValues(typeof(DayOfWeek))
               .Cast<DayOfWeek>()
               .Select(d => new
               {
                   Name = culture.DateTimeFormat.GetDayName(d),
                   Value = d
               })
               .ToList();

            Text = "Einstellungen für Bereitschaftsplan";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            Width = 350;
            Height = 210;

            Label lblYear = new Label
            {
                Text = "Jahr:",
                AutoSize = true,
                Left = 20,
                Top = 20
            };
            nudYear = new NumericUpDown
            {
                Left = 120,
                Top = 18,
                Width = 80,
                Minimum = 2000,
                Maximum = 2100,
                Value = initialYear
            };

            Label lblPeriod = new Label
            {
                Text = "Periode (Tage):",
                AutoSize = true,
                Left = 20,
                Top = 60
            };
            nudPeriodLength = new NumericUpDown
            {
                Left = 120,
                Top = 58,
                Width = 80,
                Minimum = 1,
                Maximum = 365,
                Value = initialPeriod
            };

            Label lblWeekday = new Label
            {
                Text = "Beginn Wochentag:",
                AutoSize = true,
                Left = 20,
                Top = 100
            };


            cmbWeekday = new ComboBox
            {
                Left = 120,
                Top = 98,
                Width = 120,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbWeekday.Items.AddRange(Enum.GetValues(typeof(DayOfWeek))
                                              .Cast<object>()
                                              .ToArray());
            cmbWeekday.DisplayMember = "Name";
            cmbWeekday.ValueMember = "Value";
            cmbWeekday.DataSource = days;
            cmbWeekday.SelectedItem = DayOfWeek.Monday;

            btnOk = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Left = 60,
                Width = 80,
                Top = 140
            };
            btnCancel = new Button
            {
                Text = "Abbrechen",
                DialogResult = DialogResult.Cancel,
                Left = 160,
                Width = 80,
                Top = 140
            };

            Controls.Add(lblYear);
            Controls.Add(nudYear);
            Controls.Add(lblPeriod);
            Controls.Add(nudPeriodLength);
            Controls.Add(lblWeekday);
            Controls.Add(cmbWeekday);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);

            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }
    }
}

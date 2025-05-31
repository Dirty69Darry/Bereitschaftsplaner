using System;
using System.Windows.Forms;

namespace Schichtplaner
{
    public partial class EditHolidayYearForm : Form
    {
        private NumericUpDown nudYear;
        private Button btnOk;
        private Button btnCancel;
        public int SelectedYear => (int)nudYear.Value;

        public EditHolidayYearForm(string holidayName, int currentYear)
        {
            Text = $"Jahr für {holidayName} bearbeiten";
            Width = 300; Height = 150; FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false; MinimizeBox = false; StartPosition = FormStartPosition.CenterParent;

            var lbl = new Label { Text = "Jahr:", Left = 20, Top = 20, Width = 50 };
            nudYear = new NumericUpDown { Left = 80, Top = 18, Width = 100, Minimum = 2000, Maximum = 2100, Value = currentYear > 0 ? currentYear : DateTime.Now.Year };

            btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Left = 50, Width = 80, Top = 60 };
            btnCancel = new Button { Text = "Abbrechen", DialogResult = DialogResult.Cancel, Left = 150, Width = 80, Top = 60 };

            Controls.Add(lbl); Controls.Add(nudYear); Controls.Add(btnOk); Controls.Add(btnCancel);
            AcceptButton = btnOk; CancelButton = btnCancel;
        }
        public EditHolidayYearForm(string holidayName)
        {
            int currentYear = DateTime.Now.Year;
            Text = $"Jahr für {holidayName} bearbeiten";
            Width = 300; Height = 150; FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false; MinimizeBox = false; StartPosition = FormStartPosition.CenterParent;

            var lbl = new Label { Text = "Jahr:", Left = 20, Top = 20, Width = 50 };
            nudYear = new NumericUpDown { Left = 80, Top = 18, Width = 100, Minimum = 2000, Maximum = 2100, Value = currentYear > 0 ? currentYear : DateTime.Now.Year };

            btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Left = 50, Width = 80, Top = 60 };
            btnCancel = new Button { Text = "Abbrechen", DialogResult = DialogResult.Cancel, Left = 150, Width = 80, Top = 60 };

            Controls.Add(lbl); Controls.Add(nudYear); Controls.Add(btnOk); Controls.Add(btnCancel);
            AcceptButton = btnOk; CancelButton = btnCancel;
        }

    }
}

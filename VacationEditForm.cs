using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bereitschaftsplaner
{
    public class EditForm : Form
    {
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label1;
        private Label label2;
        private Button btnOK;
        private Button btnCancel;

        public string Value1 => textBox1.Text;
        public string Value2 => textBox2.Text;

        public EditForm(string initial1, string initial2)
        {
            InitializeComponent();
            textBox1.Text = initial1;
            textBox2.Text = initial2;
        }

        private void InitializeComponent()
        {
            label1 = new Label { Text = "Beginn:", Location = new Point(20, 20), AutoSize = true };
            textBox1 = new TextBox { Location = new Point(100, 20), Width = 200 };
            label2 = new Label { Text = "Ende:", Location = new Point(20, 60), AutoSize = true };
            textBox2 = new TextBox { Location = new Point(100, 60), Width = 200 };

            btnOK = new Button { Text = "OK", Location = new Point(100, 100), DialogResult = DialogResult.OK };
            btnCancel = new Button { Text = "Abbrechen", Location = new Point(200, 100), DialogResult = DialogResult.Cancel };

            ClientSize = new Size(350, 150);
            Controls.AddRange(new Control[] { label1, textBox1, label2, textBox2, btnOK, btnCancel });

            AcceptButton = btnOK;
            CancelButton = btnCancel;
            Text = "Eintrag bearbeiten";
        }
    }
}

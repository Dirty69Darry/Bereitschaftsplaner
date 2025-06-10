namespace Bereitschaftsplaner
{
    partial class Add_Employee_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Add_Employee_Form));
            this.Close_Button = new System.Windows.Forms.Button();
            this.Forename_Label = new System.Windows.Forms.Label();
            this.Surname_Label = new System.Windows.Forms.Label();
            this.dateTimePicker_Begin = new System.Windows.Forms.DateTimePicker();
            this.Forname_Textbox = new System.Windows.Forms.MaskedTextBox();
            this.Surname_Textbox = new System.Windows.Forms.MaskedTextBox();
            this.dateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.Add_Holidays_Wish_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.VacationGroup = new System.Windows.Forms.GroupBox();
            this.Vacation_ListView = new System.Windows.Forms.ListView();
            this.Vacation_Delete_Button = new System.Windows.Forms.Button();
            this.HolidayGroup = new System.Windows.Forms.GroupBox();
            this.Holidays_ListView = new System.Windows.Forms.ListView();
            this.Add_New_Employee_Button = new System.Windows.Forms.Button();
            this.VacationGroup.SuspendLayout();
            this.HolidayGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // Close_Button
            // 
            this.Close_Button.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Close_Button.Location = new System.Drawing.Point(0, 437);
            this.Close_Button.Name = "Close_Button";
            this.Close_Button.Size = new System.Drawing.Size(800, 40);
            this.Close_Button.TabIndex = 0;
            this.Close_Button.Text = "Abbrechen";
            this.Close_Button.UseVisualStyleBackColor = true;
            this.Close_Button.Click += new System.EventHandler(this.Close_Button_Click);
            // 
            // Forename_Label
            // 
            this.Forename_Label.AutoSize = true;
            this.Forename_Label.Location = new System.Drawing.Point(24, 45);
            this.Forename_Label.Name = "Forename_Label";
            this.Forename_Label.Size = new System.Drawing.Size(49, 13);
            this.Forename_Label.TabIndex = 3;
            this.Forename_Label.Text = "Vorname";
            // 
            // Surname_Label
            // 
            this.Surname_Label.AutoSize = true;
            this.Surname_Label.Location = new System.Drawing.Point(24, 73);
            this.Surname_Label.Name = "Surname_Label";
            this.Surname_Label.Size = new System.Drawing.Size(59, 13);
            this.Surname_Label.TabIndex = 4;
            this.Surname_Label.Text = "Nachname";
            // 
            // dateTimePicker_Begin
            // 
            this.dateTimePicker_Begin.Location = new System.Drawing.Point(60, 19);
            this.dateTimePicker_Begin.Name = "dateTimePicker_Begin";
            this.dateTimePicker_Begin.Size = new System.Drawing.Size(250, 20);
            this.dateTimePicker_Begin.TabIndex = 7;
            // 
            // Forname_Textbox
            // 
            this.Forname_Textbox.Location = new System.Drawing.Point(92, 42);
            this.Forname_Textbox.Name = "Forname_Textbox";
            this.Forname_Textbox.Size = new System.Drawing.Size(321, 20);
            this.Forname_Textbox.TabIndex = 8;
            // 
            // Surname_Textbox
            // 
            this.Surname_Textbox.Location = new System.Drawing.Point(92, 70);
            this.Surname_Textbox.Name = "Surname_Textbox";
            this.Surname_Textbox.Size = new System.Drawing.Size(321, 20);
            this.Surname_Textbox.TabIndex = 9;
            // 
            // dateTimePicker_End
            // 
            this.dateTimePicker_End.Location = new System.Drawing.Point(60, 45);
            this.dateTimePicker_End.Name = "dateTimePicker_End";
            this.dateTimePicker_End.Size = new System.Drawing.Size(250, 20);
            this.dateTimePicker_End.TabIndex = 10;
            // 
            // Add_Holidays_Wish_Button
            // 
            this.Add_Holidays_Wish_Button.Location = new System.Drawing.Point(73, 71);
            this.Add_Holidays_Wish_Button.Name = "Add_Holidays_Wish_Button";
            this.Add_Holidays_Wish_Button.Size = new System.Drawing.Size(103, 31);
            this.Add_Holidays_Wish_Button.TabIndex = 11;
            this.Add_Holidays_Wish_Button.Text = "Urlaub hinzufügen";
            this.Add_Holidays_Wish_Button.UseVisualStyleBackColor = true;
            this.Add_Holidays_Wish_Button.Click += new System.EventHandler(this.Add_Holidays_Wish_Button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Urlaub";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Beginn";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Ende";
            // 
            // VacationGroup
            // 
            this.VacationGroup.Controls.Add(this.Vacation_ListView);
            this.VacationGroup.Controls.Add(this.Vacation_Delete_Button);
            this.VacationGroup.Controls.Add(this.dateTimePicker_Begin);
            this.VacationGroup.Controls.Add(this.label3);
            this.VacationGroup.Controls.Add(this.dateTimePicker_End);
            this.VacationGroup.Controls.Add(this.label2);
            this.VacationGroup.Controls.Add(this.Add_Holidays_Wish_Button);
            this.VacationGroup.Controls.Add(this.label1);
            this.VacationGroup.Location = new System.Drawing.Point(27, 113);
            this.VacationGroup.Name = "VacationGroup";
            this.VacationGroup.Size = new System.Drawing.Size(317, 263);
            this.VacationGroup.TabIndex = 16;
            this.VacationGroup.TabStop = false;
            this.VacationGroup.Text = "Urlaub bearbeiten";
            // 
            // Vacation_ListView
            // 
            this.Vacation_ListView.HideSelection = false;
            this.Vacation_ListView.Location = new System.Drawing.Point(65, 108);
            this.Vacation_ListView.Name = "Vacation_ListView";
            this.Vacation_ListView.Size = new System.Drawing.Size(242, 147);
            this.Vacation_ListView.TabIndex = 17;
            this.Vacation_ListView.UseCompatibleStateImageBehavior = false;
            this.Vacation_ListView.View = System.Windows.Forms.View.Details;
            this.Vacation_ListView.DoubleClick += new System.EventHandler(this.Vacation_ListView_SelectedIndexChanged);
            // 
            // Vacation_Delete_Button
            // 
            this.Vacation_Delete_Button.Location = new System.Drawing.Point(196, 71);
            this.Vacation_Delete_Button.Name = "Vacation_Delete_Button";
            this.Vacation_Delete_Button.Size = new System.Drawing.Size(104, 31);
            this.Vacation_Delete_Button.TabIndex = 16;
            this.Vacation_Delete_Button.Text = "Urlaub löschen";
            this.Vacation_Delete_Button.UseVisualStyleBackColor = true;
            this.Vacation_Delete_Button.Click += new System.EventHandler(this.Vacation_Delete_Button_Click);
            // 
            // HolidayGroup
            // 
            this.HolidayGroup.Controls.Add(this.Holidays_ListView);
            this.HolidayGroup.Location = new System.Drawing.Point(367, 113);
            this.HolidayGroup.Name = "HolidayGroup";
            this.HolidayGroup.Size = new System.Drawing.Size(315, 263);
            this.HolidayGroup.TabIndex = 17;
            this.HolidayGroup.TabStop = false;
            this.HolidayGroup.Text = "Bereitschaft an Feiertagen";
            // 
            // Holidays_ListView
            // 
            this.Holidays_ListView.HideSelection = false;
            this.Holidays_ListView.Location = new System.Drawing.Point(6, 19);
            this.Holidays_ListView.Name = "Holidays_ListView";
            this.Holidays_ListView.Size = new System.Drawing.Size(303, 238);
            this.Holidays_ListView.TabIndex = 0;
            this.Holidays_ListView.UseCompatibleStateImageBehavior = false;
            this.Holidays_ListView.SelectedIndexChanged += new System.EventHandler(this.Holidays_ListView_SelectedIndexChanged);
            // 
            // Add_New_Employee_Button
            // 
            this.Add_New_Employee_Button.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Add_New_Employee_Button.Location = new System.Drawing.Point(0, 382);
            this.Add_New_Employee_Button.Name = "Add_New_Employee_Button";
            this.Add_New_Employee_Button.Size = new System.Drawing.Size(800, 55);
            this.Add_New_Employee_Button.TabIndex = 18;
            this.Add_New_Employee_Button.Text = "Neuen Mitarbeiter Anlegen";
            this.Add_New_Employee_Button.UseVisualStyleBackColor = true;
            this.Add_New_Employee_Button.Click += new System.EventHandler(this.Add_New_Employee_Button_Click);
            // 
            // Add_Employee_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 477);
            this.Controls.Add(this.Add_New_Employee_Button);
            this.Controls.Add(this.HolidayGroup);
            this.Controls.Add(this.VacationGroup);
            this.Controls.Add(this.Surname_Textbox);
            this.Controls.Add(this.Forname_Textbox);
            this.Controls.Add(this.Surname_Label);
            this.Controls.Add(this.Forename_Label);
            this.Controls.Add(this.Close_Button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Add_Employee_Form";
            this.Text = "Mitarbeiter hinzufügen";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Add_Employee_Form_FormClosed);
            this.VacationGroup.ResumeLayout(false);
            this.VacationGroup.PerformLayout();
            this.HolidayGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Close_Button;
        private System.Windows.Forms.Label Forename_Label;
        private System.Windows.Forms.Label Surname_Label;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Begin;
        private System.Windows.Forms.MaskedTextBox Forname_Textbox;
        private System.Windows.Forms.MaskedTextBox Surname_Textbox;
        private System.Windows.Forms.DateTimePicker dateTimePicker_End;
        private System.Windows.Forms.Button Add_Holidays_Wish_Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox VacationGroup;
        private System.Windows.Forms.Button Vacation_Delete_Button;
        private System.Windows.Forms.GroupBox HolidayGroup;
        private System.Windows.Forms.ListView Vacation_ListView;
        private System.Windows.Forms.ListView Holidays_ListView;
        private System.Windows.Forms.Button Add_New_Employee_Button;
    }
}
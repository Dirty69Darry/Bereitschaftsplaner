namespace Bereitschaftsplaner
{
    partial class Main_Form
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.Add_Employee_Button = new System.Windows.Forms.Button();
            this.Close_Button = new System.Windows.Forms.Button();
            this.Employee_ListView = new System.Windows.Forms.ListView();
            this.Firstname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Surname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Load_Employee_Button = new System.Windows.Forms.Button();
            this.Change_Employee_Button = new System.Windows.Forms.Button();
            this.Create_ShiftPlaner_Button = new System.Windows.Forms.Button();
            this.Plan_Group = new System.Windows.Forms.GroupBox();
            this.Plan_Viewer = new System.Windows.Forms.ListView();
            this.Export_Plan_Button = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lb_Progress = new System.Windows.Forms.Label();
            this.Plan_Group.SuspendLayout();
            this.SuspendLayout();
            // 
            // Add_Employee_Button
            // 
            this.Add_Employee_Button.Location = new System.Drawing.Point(12, 46);
            this.Add_Employee_Button.Name = "Add_Employee_Button";
            this.Add_Employee_Button.Size = new System.Drawing.Size(165, 23);
            this.Add_Employee_Button.TabIndex = 0;
            this.Add_Employee_Button.Text = "Neuen Mitarbeiter Hinzufügen";
            this.Add_Employee_Button.UseVisualStyleBackColor = true;
            this.Add_Employee_Button.Click += new System.EventHandler(this.Add_Employee_Button_Click);
            // 
            // Close_Button
            // 
            this.Close_Button.Location = new System.Drawing.Point(713, 449);
            this.Close_Button.Name = "Close_Button";
            this.Close_Button.Size = new System.Drawing.Size(75, 23);
            this.Close_Button.TabIndex = 1;
            this.Close_Button.Text = "Schließen";
            this.Close_Button.UseVisualStyleBackColor = true;
            this.Close_Button.Click += new System.EventHandler(this.Close_Button_Click);
            // 
            // Employee_ListView
            // 
            this.Employee_ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Firstname,
            this.Surname});
            this.Employee_ListView.FullRowSelect = true;
            this.Employee_ListView.GridLines = true;
            this.Employee_ListView.HideSelection = false;
            this.Employee_ListView.Location = new System.Drawing.Point(12, 104);
            this.Employee_ListView.Name = "Employee_ListView";
            this.Employee_ListView.Size = new System.Drawing.Size(249, 365);
            this.Employee_ListView.TabIndex = 2;
            this.Employee_ListView.UseCompatibleStateImageBehavior = false;
            this.Employee_ListView.View = System.Windows.Forms.View.Details;
            this.Employee_ListView.SelectedIndexChanged += new System.EventHandler(this.Employee_ListView_SelectedIndexChanged);
            // 
            // Firstname
            // 
            this.Firstname.Text = "Vorname";
            this.Firstname.Width = 120;
            // 
            // Surname
            // 
            this.Surname.Text = "Nachname";
            this.Surname.Width = 120;
            // 
            // Load_Employee_Button
            // 
            this.Load_Employee_Button.Location = new System.Drawing.Point(12, 75);
            this.Load_Employee_Button.Name = "Load_Employee_Button";
            this.Load_Employee_Button.Size = new System.Drawing.Size(165, 23);
            this.Load_Employee_Button.TabIndex = 3;
            this.Load_Employee_Button.Text = "Mitarbeiter Laden";
            this.Load_Employee_Button.UseVisualStyleBackColor = true;
            this.Load_Employee_Button.Click += new System.EventHandler(this.Load_Employee_Button_Click);
            // 
            // Change_Employee_Button
            // 
            this.Change_Employee_Button.Location = new System.Drawing.Point(184, 47);
            this.Change_Employee_Button.Name = "Change_Employee_Button";
            this.Change_Employee_Button.Size = new System.Drawing.Size(124, 23);
            this.Change_Employee_Button.TabIndex = 4;
            this.Change_Employee_Button.Text = "Mitarbeiter Bearbeiten";
            this.Change_Employee_Button.UseVisualStyleBackColor = true;
            this.Change_Employee_Button.Click += new System.EventHandler(this.Change_Employee_Button_Click);
            // 
            // Create_ShiftPlaner_Button
            // 
            this.Create_ShiftPlaner_Button.Location = new System.Drawing.Point(314, 47);
            this.Create_ShiftPlaner_Button.Name = "Create_ShiftPlaner_Button";
            this.Create_ShiftPlaner_Button.Size = new System.Drawing.Size(168, 51);
            this.Create_ShiftPlaner_Button.TabIndex = 5;
            this.Create_ShiftPlaner_Button.Text = "Bereitschaftsplan erstellen";
            this.Create_ShiftPlaner_Button.UseVisualStyleBackColor = true;
            this.Create_ShiftPlaner_Button.Click += new System.EventHandler(this.Create_ShiftPlaner_Button_Click);
            // 
            // Plan_Group
            // 
            this.Plan_Group.Controls.Add(this.Plan_Viewer);
            this.Plan_Group.Location = new System.Drawing.Point(322, 109);
            this.Plan_Group.Name = "Plan_Group";
            this.Plan_Group.Size = new System.Drawing.Size(465, 325);
            this.Plan_Group.TabIndex = 6;
            this.Plan_Group.TabStop = false;
            this.Plan_Group.Text = "Bereitschaftsplan";
            // 
            // Plan_Viewer
            // 
            this.Plan_Viewer.HideSelection = false;
            this.Plan_Viewer.Location = new System.Drawing.Point(6, 19);
            this.Plan_Viewer.Name = "Plan_Viewer";
            this.Plan_Viewer.Size = new System.Drawing.Size(453, 300);
            this.Plan_Viewer.TabIndex = 0;
            this.Plan_Viewer.UseCompatibleStateImageBehavior = false;
            // 
            // Export_Plan_Button
            // 
            this.Export_Plan_Button.Location = new System.Drawing.Point(573, 46);
            this.Export_Plan_Button.Name = "Export_Plan_Button";
            this.Export_Plan_Button.Size = new System.Drawing.Size(208, 51);
            this.Export_Plan_Button.TabIndex = 7;
            this.Export_Plan_Button.Text = "Bereitschaftsplan exportieren";
            this.Export_Plan_Button.UseVisualStyleBackColor = true;
            this.Export_Plan_Button.Click += new System.EventHandler(this.Export_Plan_Button_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 475);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(775, 21);
            this.progressBar1.TabIndex = 8;
            // 
            // lb_Progress
            // 
            this.lb_Progress.AutoSize = true;
            this.lb_Progress.ForeColor = System.Drawing.Color.LimeGreen;
            this.lb_Progress.Location = new System.Drawing.Point(13, 482);
            this.lb_Progress.Name = "lb_Progress";
            this.lb_Progress.Size = new System.Drawing.Size(0, 13);
            this.lb_Progress.TabIndex = 9;
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(816, 500);
            this.Controls.Add(this.lb_Progress);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Export_Plan_Button);
            this.Controls.Add(this.Plan_Group);
            this.Controls.Add(this.Create_ShiftPlaner_Button);
            this.Controls.Add(this.Change_Employee_Button);
            this.Controls.Add(this.Load_Employee_Button);
            this.Controls.Add(this.Employee_ListView);
            this.Controls.Add(this.Close_Button);
            this.Controls.Add(this.Add_Employee_Button);
            this.Name = "Main_Form";
            this.Text = "Bereitschaftsplan";
            this.Plan_Group.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Add_Employee_Button;
        private System.Windows.Forms.Button Close_Button;
        private System.Windows.Forms.ListView Employee_ListView;
        private System.Windows.Forms.Button Load_Employee_Button;
        private System.Windows.Forms.ColumnHeader Firstname;
        private System.Windows.Forms.ColumnHeader Surname;
        private System.Windows.Forms.Button Change_Employee_Button;
        private System.Windows.Forms.Button Create_ShiftPlaner_Button;
        private System.Windows.Forms.GroupBox Plan_Group;
        private System.Windows.Forms.ListView Plan_Viewer;
        private System.Windows.Forms.Button Export_Plan_Button;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lb_Progress;
    }
}


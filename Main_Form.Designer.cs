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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Form));
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.öffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.speichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speichernunterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inhaltToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suchenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Plan_Group.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            this.Employee_ListView.DoubleClick += new System.EventHandler(this.Change_Employee_Button_Click);
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
            this.Load_Employee_Button.Text = "Team Laden";
            this.Load_Employee_Button.UseVisualStyleBackColor = true;
            this.Load_Employee_Button.Click += new System.EventHandler(this.öffnenToolStripMenuItem_Click);
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.hilfeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(816, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neuToolStripMenuItem,
            this.öffnenToolStripMenuItem,
            this.toolStripSeparator,
            this.speichernToolStripMenuItem,
            this.speichernunterToolStripMenuItem,
            this.toolStripSeparator1,
            this.beendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "&Datei";
            // 
            // neuToolStripMenuItem
            // 
            this.neuToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("neuToolStripMenuItem.Image")));
            this.neuToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.neuToolStripMenuItem.Name = "neuToolStripMenuItem";
            this.neuToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.neuToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.neuToolStripMenuItem.Text = "&Neu";
            this.neuToolStripMenuItem.Click += new System.EventHandler(this.neuToolStripMenuItem_Click);
            // 
            // öffnenToolStripMenuItem
            // 
            this.öffnenToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("öffnenToolStripMenuItem.Image")));
            this.öffnenToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.öffnenToolStripMenuItem.Name = "öffnenToolStripMenuItem";
            this.öffnenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.öffnenToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.öffnenToolStripMenuItem.Text = "Ö&ffnen";
            this.öffnenToolStripMenuItem.Click += new System.EventHandler(this.öffnenToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(165, 6);
            // 
            // speichernToolStripMenuItem
            // 
            this.speichernToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("speichernToolStripMenuItem.Image")));
            this.speichernToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.speichernToolStripMenuItem.Name = "speichernToolStripMenuItem";
            this.speichernToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.speichernToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.speichernToolStripMenuItem.Text = "&Speichern";
            // 
            // speichernunterToolStripMenuItem
            // 
            this.speichernunterToolStripMenuItem.Name = "speichernunterToolStripMenuItem";
            this.speichernunterToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.speichernunterToolStripMenuItem.Text = "Speichern &unter";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.beendenToolStripMenuItem.Text = "&Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.Close_Button_Click);
            // 
            // hilfeToolStripMenuItem
            // 
            this.hilfeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inhaltToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.suchenToolStripMenuItem,
            this.toolStripSeparator5,
            this.infoToolStripMenuItem});
            this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
            this.hilfeToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.hilfeToolStripMenuItem.Text = "&Hilfe";
            // 
            // inhaltToolStripMenuItem
            // 
            this.inhaltToolStripMenuItem.Name = "inhaltToolStripMenuItem";
            this.inhaltToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.inhaltToolStripMenuItem.Text = "I&nhalt";
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.indexToolStripMenuItem.Text = "&Index";
            // 
            // suchenToolStripMenuItem
            // 
            this.suchenToolStripMenuItem.Name = "suchenToolStripMenuItem";
            this.suchenToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.suchenToolStripMenuItem.Text = "&Suchen";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(110, 6);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.infoToolStripMenuItem.Text = "Inf&o...";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
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
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main_Form";
            this.Text = "Bereitschaftsplan";
            this.Load += new System.EventHandler(this.Main_Form_Load);
            this.Plan_Group.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem öffnenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem speichernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speichernunterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hilfeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inhaltToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suchenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
    }
}


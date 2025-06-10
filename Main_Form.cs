using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Bereitschaftsplaner.Program;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bereitschaftsplaner
{
    public partial class Main_Form : Form
    {
        private BackgroundWorker exportWorker;
        public Guid selectedEmployeeIndex = Guid.Empty;
        private EncryptedRepository<Employee> empRepo;
        DayOfWeek firstWeekday = DayOfWeek.Monday;
        int shiftPeriod = 7;

        [DllImport("shell32.dll")]
        static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);
        const uint SHCNE_UPDATEDIR = 0x00002000;
        const uint SHCNF_PATHW = 0x0005;

        public Main_Form()
        {
            InitializeComponent();


            //Ordnerstruktur anlegen
            AppPaths.EnsureAllDirectoriesExist();



            byte[] key = Global.globalKEY;
            empRepo = new EncryptedRepository<Employee>(Global.employeeDataPath, key);
            
        
            //Progress für Export
            //Backgroundworker initialisieren
            exportWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,   // Erlaubt Fortschrittsmeldungen
                WorkerSupportsCancellation = false
            };
            // Event-Handler zuweisen:
            exportWorker.DoWork += ExportWorker_DoWork;
            exportWorker.ProgressChanged += ExportWorker_ProgressChanged;
            exportWorker.RunWorkerCompleted += ExportWorker_RunWorkerCompleted;

        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            if (Directory.GetFiles(Global.defaultDirectory, "*.team").Length <= 0)
            {
                neuToolStripMenuItem_Click(sender, e);
            }

            this.Text = $"Bereitschaftsplan von {Path.GetFileNameWithoutExtension(Global.employeeDataPath)}";
            LoadEmployeesIntoListBox();
        }

        private void RefreshShellFolder(string path)
        {
            SHChangeNotify(SHCNE_UPDATEDIR, SHCNF_PATHW, Marshal.StringToHGlobalUni(path), IntPtr.Zero);
        }

        public void LoadEmployeesIntoListBox()
        {
            try
            {
                var employees = empRepo.Load();
                Employee_ListView.Items.Clear();

                foreach (var emp in employees)
                {
                    var item = new ListViewItem(emp.FirstName);
                    item.SubItems.Add(emp.LastName);
                    item.Tag = emp.Id;
                    Employee_ListView.Items.Add(item);

                }
            }
            catch (Exception ex) {MessageBox.Show(ex.Message); }   
            
        }

        private void Add_Employee_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadEmployeesIntoListBox();
        }

        private int GetWorkdays(DateTime start, DateTime ende)
        {
            return (int)(ende - start).TotalDays;
        }

        private DateTime ErsteKalenderwocheStart(int jahr)
        {
            // Erste Kalenderwoche beginnt mit dem ersten Montag vor/neben dem 1. Januar
            DateTime jan1 = new DateTime(jahr, 1, 1);
            while (jan1.DayOfWeek != DayOfWeek.Monday)
                jan1 = jan1.AddDays(-1);
            return jan1;
        }

        private DateTime GetFirstWeekdayOfYear(int year, DayOfWeek weekday)
        {
            DateTime dt = new DateTime(year, 1, 1);
            while (dt.DayOfWeek != weekday)
            {
                dt = dt.AddDays(1);
            }
            return dt;
        }

        private void ExportWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = Math.Min(e.ProgressPercentage, progressBar1.Maximum);
            lb_Progress.Text = $"Fortschritt: {e.ProgressPercentage}%";

        }

        private void ExportWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                lb_Progress.Text = "Export abgebrochen.";
            }
            else if (e.Error != null)
            {
                lb_Progress.Text = $"Fehler beim Export: {e.Error.Message}";
            }
            else
            {
                lb_Progress.Text = "Export abgeschlossen.";
                MessageBox.Show("Excel-Datei erfolgreich gespeichert.", "Fertig", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            progressBar1.Value = 0;
        }

        private void ExportWorker_DoWork(object sender, DoWorkEventArgs evarg)
        {
            var args = evarg.Argument as Tuple<string, List<(DateTime Start, DateTime Ende, string Name, Guid Id)>>;
            string filePath = args.Item1;
            var eintraege = args.Item2.OrderBy(x => x.Start).ToList();

            var worker = sender as BackgroundWorker;

            int currentStep = 0;          

            // Wenn keine Datei gewählt wurde, abbrechen
            if (string.IsNullOrWhiteSpace(filePath))
            {
                evarg.Cancel = true;
                return;
            }

            // Nach Startdatum sortieren
            eintraege = eintraege.OrderBy(e => e.Start).ToList();

            // Excel-Export starten
            using (var wb = new XLWorkbook())
            {
                // Übersicht-Sheet
                var ws = wb.AddWorksheet("Übersicht");
                ws.Cell(1, 1).Value = "Start";
                ws.Cell(1, 2).Value = "Ende";
                ws.Cell(1, 3).Value = "Name";
                ws.Cell(1, 4).Value = "Tage";

                var mitarbeiterSumme = new Dictionary<string, int>();
                int zeile = 2;

                foreach (var e in eintraege)
                {
                    int werktage = GetWorkdays(e.Start, e.Ende);
                    ws.Cell(zeile, 1).Value = e.Start;
                    ws.Cell(zeile, 2).Value = e.Ende;
                    ws.Cell(zeile, 3).Value = e.Name;
                    ws.Cell(zeile, 4).Value = werktage;

                    if (!mitarbeiterSumme.ContainsKey(e.Name))
                        mitarbeiterSumme[e.Name] = 0;
                    mitarbeiterSumme[e.Name] += werktage;

                    zeile++;
                }

                //Hide Tage für Summe
                ws.Column(4).Hide();

                //Update Progess
                currentStep++;
                int totalSteps = 2 + eintraege.Select(x => x.Name).Distinct().Count();
                worker.ReportProgress(currentStep / totalSteps);

                // Gesamtsumme je Mitarbeiter (ab Zeile + 2)
                int summaryRow = zeile + 2;
                ws.Cell(summaryRow, 1).Value = "Mitarbeiter";
                ws.Cell(summaryRow, 2).Value = "Summe Wochentage";
                int i = 1;
                foreach (var pair in mitarbeiterSumme)
                {
                    ws.Cell(summaryRow + i, 1).Value = pair.Key;
                    ws.Cell(summaryRow + i, 2).Value = pair.Value;
                    i++;
                }

                ws.Columns().AdjustToContents();

                // Kalender-Sheets je Mitarbeiter
                var mitarbeiterGruppiert = eintraege.GroupBy(e => e.Name);
                foreach (var gruppe in mitarbeiterGruppiert)
                {
                    // Sheet-Namen bereinigen (max. 31 Zeichen, keine verbotenen Zeichen)
                    string roherName = gruppe.Key;
                    string name = Regex.Replace(roherName, @"[\\/\*\[\]\?]", "_");
                    if (name.Length > 31)
                        name = name.Substring(0, 31);

                    var sheet = wb.AddWorksheet(name);
                    int jahr = gruppe.First().Start.Year;

                    // *** 3×4-Raster für 12 Monate ***
                    // Breite und Höhe je Monats-Block definieren (inkl. Rahmen)
                    int boxWidth = 9; // 7 Spalten (Wochentage) + 2 für Ränder
                    int boxHeight = 8; // 1 Titelzeile + 6 Wochenzeilen + 1 Puffer/Unterkante

                    // 12 Monate (1 bis 12)
                    for (int month = 1; month <= 12; month++)
                    {
                        // → Berechnung in welchem „Kästchen“ (Zeile, Spalte) der Monat liegt:
                        int rasterRow = ((month - 1) / 3);
                        int rasterCol = ((month - 1) % 3);

                        // Startzeile und -spalte für dieses Kästchen
                        int startRow = rasterRow * boxHeight + 1;
                        int startCol = rasterCol * boxWidth + 1;

                        // 3.1) Monatsüberschrift zentriert (über 7 Spalten)
                        string monatName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                        var titleCell = sheet.Cell(startRow, startCol);
                        titleCell.Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(monatName);
                        titleCell.Style.Font.Bold = true;
                        titleCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        // Überschrift über 7 Zellen mergen
                        sheet.Range(startRow, startCol, startRow, startCol + 6).Merge();

                        // 3.2) Wochentagsüberschriften (Mo–So) in der nächsten Zeile
                        string[] tage = new[] { "Mo", "Di", "Mi", "Do", "Fr", "Sa", "So" };
                        for (int d = 0; d < 7; d++)
                        {
                            var c = sheet.Cell(startRow + 1, startCol + d);
                            c.Value = tage[d];
                            c.Style.Font.Italic = true;
                            c.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        }

                        // 3.3) Monatstage füllen
                        DateTime ersterDesMonats = new DateTime(jahr, month, 1);
                        // Ermittlung, an welchem Wochentag der 1. liegt: (Montag=0, Sonntag=6)
                        int anfangsOffset = ((int)ersterDesMonats.DayOfWeek + 6) % 7;

                        int tageImMonat = DateTime.DaysInMonth(jahr, month);
                        for (int tag = 1; tag <= tageImMonat; tag++)
                        {
                            DateTime aktuell = new DateTime(jahr, month, tag);
                            // Zeile im Block (ab startRow+2) und Spalte ab startCol
                            int relIndex = anfangsOffset + (tag - 1);
                            int zelleZeile = startRow + 2 + (relIndex / 7);
                            int zelleSpalte = startCol + (relIndex % 7);

                            var cell = sheet.Cell(zelleZeile, zelleSpalte);
                            cell.Value = tag;
                            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            //Bereitschaftstag markieren
                            if (gruppe.Any(g => g.Start.Date <= aktuell.Date && g.Ende.Date >= aktuell.Date))
                            {
                                cell.Style.Fill.BackgroundColor = XLColor.Yellow;
                            }

                            // Tage <> im Jahr (also Vorjahr/Nächstes Jahr) grau färben
                            if (aktuell.Year != jahr)
                            {
                                cell.Style.Font.FontColor = XLColor.Gray;
                            }


                        }

                        // 3.4) Rahmen (Border) um den gesamten Monatsblock (Box)
                        var blockRange = sheet.Range(
                            startRow,
                            startCol,
                            startRow + boxHeight - 1,
                            startCol + boxWidth - 3  // → nur 7 Spalten breit (ohne zweiten Rand)
                        );
                        blockRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        blockRange.Style.Border.InsideBorder = XLBorderStyleValues.Hair;
                    }

                    // 4) Seitenlayout für DIN A4 → Querformat, FitToPage
                    sheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                    sheet.PageSetup.PaperSize = XLPaperSize.A4Paper;
                    sheet.PageSetup.FitToPages(1, 1);
                    sheet.PageSetup.Margins.Top = 0.3;
                    sheet.PageSetup.Margins.Bottom = 0.3;
                    sheet.PageSetup.Margins.Left = 0.3;
                    sheet.PageSetup.Margins.Right = 0.3;

                    // Spaltenbreiten an Inhalt anpassen
                    sheet.Columns().AdjustToContents();

                    //Update Progress
                    currentStep++;
                    worker.ReportProgress(currentStep * 100/ totalSteps);
                }

                try
                {
                    wb.SaveAs(filePath);

                    //Final Update Progress
                    worker.ReportProgress(100);
                }
                catch (IOException ioEx)
                {
                    MessageBox.Show(
                        $"Die Datei konnte nicht gespeichert werden.\n\nMöglicherweise ist sie bereits geöffnet oder gesperrt.\n\nFehler: {ioEx.Message}",
                        "Fehler beim Speichern",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    evarg.Cancel = true; // optional: Abbruch signalisieren
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Beim Export ist ein unerwarteter Fehler aufgetreten:\n\n{ex.Message}",
                        "Allgemeiner Fehler",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    evarg.Cancel = true;
                }              
            }

        }

        private void Close_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Add_Employee_Button_Click(object sender, EventArgs e)
        {

            Add_Employee_Form add_employee = new Add_Employee_Form();
            add_employee.Show();
            add_employee.FormClosed += Add_Employee_Form_FormClosed;
        }

        private void Change_Employee_Button_Click(object sender, EventArgs e)
        {
            if (selectedEmployeeIndex != Guid.Empty)
            {
                Change_Employee_Form change_Employee_Form = new Change_Employee_Form(selectedEmployeeIndex);
                change_Employee_Form.Show();
            }
            else { MessageBox.Show("Wählen Sie einen Mitarbeiter aus"); }

        }

        private void Employee_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Employee_ListView.SelectedItems.Count == 0) return;
            
            ListViewItem selectedItem = Employee_ListView.SelectedItems[0];

            if (selectedItem.Tag is Guid employeeID)
            {
                selectedEmployeeIndex = employeeID;
            }
        }

        private void Create_ShiftPlaner_Button_Click(object sender, EventArgs e)
        {

            // 1. Optionen-Dialog anzeigen
            int currentYear = DateTime.Now.Year;
            int defaultPeriod = 7;
            using (var dlg = new ShiftPlanOptionsForm(currentYear, defaultPeriod))
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                Global.selected_year = dlg.SelectedYear;
                shiftPeriod = dlg.PeriodLengthDays;
                firstWeekday = dlg.FirstWeekday;

                //erster gewünschter Wochentag im Jahr
                DateTime startDate = GetFirstWeekdayOfYear(Global.selected_year, firstWeekday);

                DateTime endDate = new DateTime(Global.selected_year + 1, 1, 1);

                DateTime effectiveEnd;
                if (shiftPeriod % 7 == 0)
                {
                    // Gesamttage im Jahr ab startDate bis zum Stichtag
                    int totalDays = (int)(endDate - startDate).TotalDays;
                    // Anzahl benötigter Perioden
                    int fullPeriods = (int)Math.Ceiling(totalDays / (double)shiftPeriod);

                    effectiveEnd = startDate.AddDays(fullPeriods * shiftPeriod);
                    endDate = effectiveEnd;
                }
                else
                {
                    // Normales Ende (kein ganzwöchiger Rhythmus)
                    effectiveEnd = endDate;
                }

                TimeSpan timeSpan = endDate - startDate;
                int expectedShifts = timeSpan.Days / shiftPeriod;

                // 3. Mitarbeiter und Feiertage laden
                var employees = new EmployeeManager(Global.employeeDataPath, Encoding.UTF8.GetBytes("16ByteSecretKey!")).LoadAll();
                var holidays = HolidayCalculator.GetHolidays(Global.selected_year);

                // 4. Plan erstellen
                var planner = new ShiftPlanner(employees, holidays, rotationPeriodDays: shiftPeriod);
                var plan = planner.GeneratePlan(startDate, endDate);

                // 5. ListView konfigurieren
                Plan_Viewer.View = View.Details;
                Plan_Viewer.FullRowSelect = true;
                Plan_Viewer.GridLines = true;
                Plan_Viewer.Columns.Clear();
                Plan_Viewer.Columns.Add("Beginn", 100, HorizontalAlignment.Left);
                Plan_Viewer.Columns.Add("Ende", 100, HorizontalAlignment.Left);
                Plan_Viewer.Columns.Add("Name", 200, HorizontalAlignment.Left);

                Plan_Viewer.Items.Clear();

                if (plan.Count < expectedShifts)
                {
                    MessageBox.Show("Es konnten nicht alle Schichten belegt werden.", "Hinweis",
                                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // 6. Ausgabe: Für jeden Shift-Assignment Namen nachschlagen
                var allEmployees = employees.ToDictionary(emp => emp.Id, emp => $"{emp.FirstName} {emp.LastName}");
                foreach (var assignment in plan)
                {
                    string fullName = allEmployees.ContainsKey(assignment.EmployeeId)
                                      ? allEmployees[assignment.EmployeeId]
                                      : assignment.EmployeeId.ToString();

                    var item = new ListViewItem(assignment.Start.ToShortDateString());
                    item.SubItems.Add(assignment.End.ToShortDateString());
                    item.SubItems.Add(fullName);
                    item.Tag = assignment.EmployeeId;
                    Plan_Viewer.Items.Add(item);
                }
            }
        }

        private void Export_Plan_Button_Click(object sender, EventArgs evarg)
        {
            if (Plan_Viewer.Items.Count == 0)
            {
                MessageBox.Show("Keine Einträge vorhanden.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel-Datei (*.xlsx)|*.xlsx";
                sfd.FileName = Global.planOutputPath;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // Daten sammeln
                    var eintraege = new List<(DateTime Start, DateTime Ende, string Name, Guid Id)>();
                    foreach (ListViewItem item in Plan_Viewer.Items)
                    {
                        DateTime start = DateTime.Parse(item.SubItems[0].Text);
                        DateTime ende = DateTime.Parse(item.SubItems[1].Text);
                        string name = item.SubItems[2].Text;
                        Guid id = (Guid)item.Tag;
                        eintraege.Add((start, ende, name, id));
                    }

                    // Argumente für Export vorbereiten
                    var args = new Tuple<string, List<(DateTime, DateTime, string, Guid)>>(
                        sfd.FileName,
                        eintraege
                    );

                    //ProgressBar zurücksetzen
                    progressBar1.Value = 0;
                    lb_Progress.Text = "Bereit zum Exportieren";

                    //Starte BackgroundWorker
                    exportWorker.RunWorkerAsync(args);

                }

            }
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var version = assembly.GetName().Version?.ToString() ?? "unbekannt";

            string productName = Application.ProductName;
            string company = Application.CompanyName;
            string info = $"Programm: {productName}\n" +
                          $"Herausgeber: {company}\n" +
                          $"Version: {version}";

            MessageBox.Show(info, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);        

    }

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string defaultFolder = Global.defaultDirectory;

            // Ordner erstellen, falls er noch nicht existiert
            if (!Directory.Exists(defaultFolder))
            {
                Directory.CreateDirectory(defaultFolder);
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Teamdateien (*.team)|*.team";
                saveFileDialog.Title = "Neue Teamdatei erstellen";
                saveFileDialog.InitialDirectory = defaultFolder;
                saveFileDialog.FileName = "neues_Team.team"; 

                Employee_ListView.Items.Clear();

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    empRepo = new EncryptedRepository<Employee>(filePath, Global.globalKEY);
                    var manager = new EmployeeManager(filePath,Global.globalKEY);
                    manager.InitializeNewTeam(filePath);
                    
                    MessageBox.Show($"Datei erstellt:\n{Path.GetFileName(filePath)}", "Neu", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Text = $"Bereitschaftsplan von {Path.GetFileNameWithoutExtension(Global.employeeDataPath)}";
                    LoadEmployeesIntoListBox();
                }
            }
        }

        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string defaultDirectory = Global.defaultDirectory;

            if (!Directory.Exists(defaultDirectory))
                Directory.CreateDirectory(defaultDirectory);

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = defaultDirectory;
                openFileDialog.Filter = "Teamdateien (*.team)|*.team";
                openFileDialog.Title = "Teamdatei öffnen";
                openFileDialog.RestoreDirectory = true; //Löscht Cache beim Schließen des Dialogfensters?

                RefreshShellFolder(defaultDirectory);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        var key = Global.globalKEY;
                        var manager = new EmployeeManager(filePath, key);

                        empRepo = new EncryptedRepository<Employee>(filePath,key);
                        var employeeList = manager.LoadAll();

                        // Speichere und zeige den aktiven Pfad
                        Global.employeeDataPath = filePath;
                        
                        // Lade die Mitarbeiterliste in dein UI z. B. ListView
                        LoadEmployeesIntoListBox();

                        this.Text = $"Bereitschaftplan von {Path.GetFileNameWithoutExtension(filePath)}";


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Fehler beim Laden der Datei:\n" + ex.Message, "Ladefehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }




}

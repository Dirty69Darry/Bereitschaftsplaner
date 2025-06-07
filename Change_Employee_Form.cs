using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Bereitschaftsplaner.Program;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bereitschaftsplaner
{
    public partial class Change_Employee_Form : Form
    {
        Guid employee_index = Guid.Empty;
        Employee employee;

        public Change_Employee_Form(Guid index)
        {
            InitializeComponent();
            employee_index = index;

            //Initialisieren von Vacation_ListView
            Vacation_ListView.View = View.Details;
            Vacation_ListView.FullRowSelect = true;
            Vacation_ListView.GridLines = true;
            Vacation_ListView.Columns.Clear();
            Vacation_ListView.Columns.Add("Beginn", 120, HorizontalAlignment.Left);
            Vacation_ListView.Columns.Add("Ende", 120, HorizontalAlignment.Left);
            var vacItem = new ListViewItem("0");
            vacItem.SubItems.Add("0");
            Vacation_ListView.Items.Add(vacItem);
            Vacation_ListView.Items.Remove(vacItem);

            //HolidayListViewHelper.PopulateHolidayListView(this.Holidays_ListView);

            Change_Employee_Form_Load();

        }

        void Change_Employee_Form_Load()
        {
            byte[] key = Encoding.UTF8.GetBytes("16ByteSecretKey!");
            var employees = LoadEmployeeList(Global.employeeDataPath, key);
            
            if (employees.Any())
            {
                try
                {
                    Console.WriteLine("in Loading");
                    employee = FindEmployeeById(employee_index, employees);

                    this.Text = employee.FirstName + " " + employee.LastName + " bearbeiten";
                    Firstname_Textbox.Text = employee.FirstName;
                    Surname_Textbox.Text = employee.LastName;

                    foreach (var vacation in employee.Vacations)
                    {
                        var item = new ListViewItem(vacation.StartDate.ToShortDateString());
                        item.SubItems.Add(vacation.EndDate.Date.ToShortDateString());
                        Vacation_ListView.Items.Add(item);
                        Console.WriteLine(item.Text + "-> added.");
                    }

                    //Feiertage laden
                    Holidays_ListView.Columns.Clear();
                    Holidays_ListView.Items.Clear();

                    Holidays_ListView.View = View.Details;
                    Holidays_ListView.FullRowSelect = true;
                    Holidays_ListView.GridLines = true;

                    Holidays_ListView.Columns.Add("Feiertag", 150, HorizontalAlignment.Left);
                    Holidays_ListView.Columns.Add("Jahr", 100, HorizontalAlignment.Left);

                    foreach (HolidayType holiday in Enum.GetValues(typeof(HolidayType)))
                    {
                        string yearString = employee.LastOnHolidayYear.TryGetValue(holiday, out int year) ? year.ToString() : "";
                        var item = new ListViewItem(holiday.ToString());
                        item.SubItems.Add(yearString);
                        Holidays_ListView.Items.Add(item);
                    }                    
                }
                catch { MessageBox.Show("Some Error"); }
            }
        }

        private Employee FindEmployeeById(Guid id, List<Employee> employees)
        {
            return employees.FirstOrDefault(emp => emp.Id == id);
        }

        private List<Employee> LoadEmployeeList(string filePath, byte[] encryptionKey)
        {
            try
            {
                var repository = new EncryptedRepository<Employee>(filePath, encryptionKey);
                var employees = repository.Load();
                return employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden der Mitarbeiterdaten:\n{ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Employee>();
            }
        }

        private void Save_Changes_Button_Click(object sender, EventArgs e)
        {
            byte[] key = Encoding.UTF8.GetBytes("16ByteSecretKey!");
            var manager = new EmployeeManager(Global.employeeDataPath, key);


            var emp = new Employee
            {
                FirstName = Firstname_Textbox.Text,
                LastName = Surname_Textbox.Text,
                Id = employee_index
            };

            foreach (ListViewItem item in Vacation_ListView.Items)
            {
                if (DateTime.TryParse(item.SubItems[0].Text, out DateTime vac_begin) && DateTime.TryParse(item.SubItems[1].Text, out DateTime vac_end))
                    emp.AddVacation(vac_begin, vac_end);
            }

            foreach (ListViewItem item in Holidays_ListView.Items)
            {
                string holidayType = item.SubItems[0].Text;
                if (Int32.TryParse(item.SubItems[1].Text, out int year) &&
                    Enum.TryParse<HolidayType>(holidayType, out var result) &&
                    Enum.IsDefined(typeof(HolidayType), result))
                {
                    emp.UpdateLastHolidayYear((HolidayType)result, year);
                    Console.WriteLine("Feiertag gespeichert");
                }
            }
            {
                manager.SaveOrUpdateEmployee(emp);
                MessageBox.Show("Änderungen wurden gespeichert.");
                if (System.Windows.Forms.Application.OpenForms["Main_Form"] != null)
                {
                    (System.Windows.Forms.Application.OpenForms["Main_Form"] as Main_Form).LoadEmployeesIntoListBox();
                }
                this.Close();
            }         
        }

        private void Delete_Button_Click(object sender, EventArgs e)
        {
            byte[] key = Encoding.UTF8.GetBytes("16ByteSecretKey!");
            var manager = new EmployeeManager(Global.employeeDataPath, key);

            manager.DeleteEmployee(employee_index);

            if (!manager.Exists(employee_index))
            {
                MessageBox.Show("Mitarbeiter ist gelöscht.");
                if(System.Windows.Forms.Application.OpenForms["Main_Form"] != null)
                { 
                (System.Windows.Forms.Application.OpenForms["Main_Form"] as Main_Form).LoadEmployeesIntoListBox();
                }
                this.Close();
            }
            else
                MessageBox.Show("Beim Löschen ist ein Fehler passiert.");
        }

        private void Close_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Add_Holidays_Wish_Button_Click(object sender, EventArgs e)
        {
            string holidays_begin = dateTimePicker_Begin.Value.ToShortDateString();
            string holidays_end = dateTimePicker_End.Value.ToShortDateString();
        

            if (dateTimePicker_Begin.Value.Date <= dateTimePicker_End.Value.Date)
            {
                var vac_item = new ListViewItem(holidays_begin);
                vac_item.SubItems.Add(holidays_end);
                Vacation_ListView.Items.Add(vac_item);

                ResortListViewByColumn(Vacation_ListView, 0, SortOrder.Ascending);

            }
            else
            {
                MessageBox.Show(
                    "Sie haben ein falsches Datum eingegeben.\n" + holidays_begin + " ist ein spätereres Datum als " + holidays_end,
                    "Ungültiges Datumformat",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                    );
            }
        }

        private void Vacation_Delete_Button_Click(object sender, EventArgs e)
        {
            DeleteSelectedRow(Vacation_ListView);
        }

        public void ResortListViewByColumn(System.Windows.Forms.ListView listView, int columnIndex, SortOrder order)
        {
            // 1) Klone alle Items in eine List<ListViewItem>
            var items = listView.Items
                                .Cast<ListViewItem>()
                                .Select(i => (ListViewItem)i.Clone())
                                .ToList();

            // 2) Sortiere die Liste
            items.Sort(new ListViewItemComparer(columnIndex, order));
        
     

            // 3) Fülle die ListView neu
            listView.BeginUpdate();
            listView.Items.Clear();
            foreach (ListViewItem item in items)
            {
                Console.WriteLine(item.ToString());
                listView.Items.Add(item);
            }
            listView.EndUpdate();
        }

        private void DeleteSelectedRow(System.Windows.Forms.ListView listView)
        {
            if (listView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in listView.SelectedItems)
                {
                    listView.Items.Remove(item);
                }
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie eine Zeile zum Löschen aus.", "Keine Auswahl", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Holidays_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Holidays_ListView.SelectedItems.Count == 0) return;

            ListViewItem selectedItem = Holidays_ListView.SelectedItems[0];

            if (selectedItem != null)
            {
                if (HelperFunctions.StringToInt(selectedItem.SubItems[1].Text, out int year))
                    using (var editHolidayForm = new EditHolidayYearForm(selectedItem.SubItems[0].Text, year))
                    {
                        if (editHolidayForm.ShowDialog() == DialogResult.OK)
                        {
                            {
                                try
                                {
                                    selectedItem.SubItems[1].Text = editHolidayForm.SelectedYear.ToString();
                                }
                                catch
                                {
                                    MessageBox.Show(
                                        "Sie haben ein ungültieges Jahr eingegeben.\nDas Format ist YYYY\nZum Beispiel 2025.",
                                        "Ungültiges Datumformat",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation
                                        );
                                }
                            }
                        }
                    }
                else
                {
                    using (var editHolidayForm = new EditHolidayYearForm(selectedItem.SubItems[0].Text))
                    {
                        if (editHolidayForm.ShowDialog() == DialogResult.OK)
                        {
                            {
                                try
                                {
                                    selectedItem.SubItems[1].Text = editHolidayForm.SelectedYear.ToString();
                                }
                                catch
                                {
                                    MessageBox.Show(
                                        "Sie haben ein ungültieges Jahr eingegeben.\nDas Format ist YYYY\nZum Beispiel 2025.",
                                        "Ungültiges Datumformat",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation
                                        );
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

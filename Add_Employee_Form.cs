using System;
using System.Collections;
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
    public partial class Add_Employee_Form : Form
    {
        public Add_Employee_Form()
        {
            InitializeComponent();

            //Initialisieren von Vacation_ListView
            Vacation_ListView.View = View.Details;
            Vacation_ListView.FullRowSelect = true; 
            Vacation_ListView.GridLines = true;
            Vacation_ListView.Columns.Clear();
            Vacation_ListView.Columns.Add("Beginn",120,HorizontalAlignment.Left);
            Vacation_ListView.Columns.Add("Ende",120,HorizontalAlignment.Left);
            var vacItem = new ListViewItem("0");
            vacItem.SubItems.Add("0");
            Vacation_ListView.Items.Add(vacItem);
            Vacation_ListView.Items.Remove(vacItem);

            //Initialisieren von Holidays_ListView
            HolidayListViewHelper.PopulateHolidayListView(Holidays_ListView);

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

        private void Holidays_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Doppelklick in Feiertage");
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

        private void Vacation_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Vacation_ListView.SelectedItems.Count == 0) return;

            ListViewItem selectedItem = Vacation_ListView.SelectedItems[0];

            if (selectedItem != null)
            {
                using (var editForm = new EditForm(selectedItem.SubItems[0].Text, selectedItem.SubItems[1].Text))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        if (DateTime.TryParse(editForm.Value1, out DateTime date1) && DateTime.TryParse(editForm.Value2, out DateTime date2))
                        {
                            if (date1.Date <= date2.Date)
                            {
                                selectedItem.SubItems[0].Text = editForm.Value1;
                                selectedItem.SubItems[1].Text = editForm.Value2;
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Sie haben ein falsches Datum eingegeben.\n" + date1.Date + " ist ein spätereres Datum als " + date2.Date,
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

        private void Add_New_Employee_Button_Click(object sender, EventArgs e)
        {
            byte[] key = Encoding.UTF8.GetBytes("16ByteSecretKey!");
            var manager = new EmployeeManager(Global.employeeDataPath, key);
            if (Forname_Textbox.Text.Length > 0 && Surname_Textbox.Text.Length > 0) 
            {
                Guid new_empID = Guid.NewGuid();

                //Neuen Mitarbeiter initialisieren
                var emp = new Employee
                {
                    FirstName = Forname_Textbox.Text,
                    LastName = Surname_Textbox.Text,
                    Id = new_empID
                };

                //Liste der Urlaube übernehmen
                foreach (ListViewItem item in Vacation_ListView.Items)
                {
                    if (DateTime.TryParse(item.SubItems[0].Text, out DateTime vac_begin) && DateTime.TryParse(item.SubItems[1].Text, out DateTime vac_end))
                    emp.AddVacation(vac_begin, vac_end); 
                }

                //Liste der Feiertage
                foreach (ListViewItem item in Holidays_ListView.Items)
                {
                    string holidayType = item.SubItems[0].Text;
                    if (Int32.TryParse(item.SubItems[1].Text, out int year) &&
                        Enum.TryParse<HolidayType>(holidayType, out var result) && 
                        Enum.IsDefined(typeof(HolidayType), result))
                    {

                        emp.UpdateLastHolidayYear((HolidayType)result, year);
                    }   
                }
                manager.SaveOrUpdateEmployee(emp);
                MessageBox.Show("Neuer Mitarbeiter wurde angelegt.");
                this.Close();
            }
            else { MessageBox.Show("Geben Sie einen Vornamen und einen Nachnamen ein"); }
        }
    }
}

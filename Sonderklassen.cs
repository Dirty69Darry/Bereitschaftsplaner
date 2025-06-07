using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static Bereitschaftsplaner.Program;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;

namespace Bereitschaftsplaner
{
    public static class AppPaths
    {
        public static string Root => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BereitschaftsDienstPlanner");
        public static string Data => Path.Combine(Root, "Data");
        public static string Plans => Path.Combine(Root, "Plans");
        public static string Logs => Path.Combine(Root, "Logs");
        public static string Backups => Path.Combine(Root, "Backups");
        public static string Output => Path.Combine(Root, "Output");
        public static string Settings => Path.Combine(Root, "Settings");

        public static void EnsureAllDirectoriesExist()
        {
            Directory.CreateDirectory(Data);
            Directory.CreateDirectory(Plans);
            Directory.CreateDirectory(Logs);
            Directory.CreateDirectory(Backups);
            Directory.CreateDirectory(Output);
            Directory.CreateDirectory(Settings);
        }
    }

    class ListViewItemComparer : IComparer<ListViewItem>
    {
        private int columnIndex;
        private SortOrder sortOrder;
        private static readonly CultureInfo German = new CultureInfo("de-DE");
        private const string DateFormat = "dd.MM.yyyy";

        public ListViewItemComparer(int column, SortOrder order)
        {
            columnIndex = column;
            sortOrder = order;
        }

        public int Compare(ListViewItem x, ListViewItem y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            string valX = ((ListViewItem)x).SubItems[columnIndex].Text.Trim();
            string valY = ((ListViewItem)y).SubItems[columnIndex].Text.Trim();

            int result = CompareSmart(valX, valY);
            Console.WriteLine(result);

            return sortOrder == SortOrder.Ascending ? result : -result;
        }

        private int CompareSmart(string a, string b)
        {

            bool xIsDate = DateTime.TryParseExact(
                a, DateFormat, German, DateTimeStyles.None, out DateTime dateA);
            bool yIsDate = DateTime.TryParseExact(
                b, DateFormat, German, DateTimeStyles.None, out DateTime dateB);

            //1. Datum
            if (xIsDate && yIsDate)
            {
                    int aInt = dateA.Year * 10000 + dateA.Month * 100 + dateA.Day;
                    int bInt = dateB.Year * 10000 + dateB.Month * 100 + dateB.Day;
                    return aInt.CompareTo(bInt);                
            }
            else { MessageBox.Show("Error im Sortiern"); }

            // 2. Zahlen
            if (double.TryParse(a, out double numA) && double.TryParse(b, out double numB))
                return numA.CompareTo(numB);

            // 3. Standard-Textvergleich
            return string.Compare(a, b, ignoreCase: true, culture: CultureInfo.CurrentCulture);
        }
    }

    public static class HolidayListViewHelper
    {
        public static void PopulateHolidayListView(ListView listView)
        {
            listView.Columns.Clear();
            listView.Items.Clear();

            listView.View = View.Details;
            listView.FullRowSelect = true;
            listView.GridLines = true;

            listView.Columns.Add("Feiertag", 150, HorizontalAlignment.Left);
            listView.Columns.Add("Jahr", 100, HorizontalAlignment.Left);

            foreach (var holiday in Enum.GetValues(typeof(HolidayType)))
            {
                var item = new ListViewItem(holiday.ToString());
                item.SubItems.Add("");
                listView.Items.Add(item);
            }
            Console.WriteLine("Feiertage wurden geladen");
        }
    }

    public static class HelperFunctions
    {
        public static bool StringToInt(string str, out int result)
        {            
            return Int32.TryParse(str, out result);
        }
    }


}

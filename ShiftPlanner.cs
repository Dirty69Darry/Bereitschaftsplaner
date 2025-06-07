using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Schichtplaner.Program;

namespace Schichtplaner
{
    internal class ShiftPlanner
    {
        private readonly List<Employee> _employees;
        private readonly List<DateTime> _holidays;
        private readonly int _rotationPeriodDays;

        /// <param name="employees">Liste aller Mitarbeiter</param>
        /// <param name="holidays">Liste aller Feiertage im Planungszeitraum</param>
        /// <param name="rotationPeriodDays">Länge einer Periode in Tagen (z.B. 7 für Wochenrotation)</param>
        public ShiftPlanner(List<Employee> employees, List<DateTime> holidays, int rotationPeriodDays)
        {
            _employees = employees;
            _holidays = holidays;
            _rotationPeriodDays = rotationPeriodDays;
        }

        public List<ShiftAssignment> GeneratePlan(DateTime start, DateTime end)
        {
            var assignments = new List<ShiftAssignment>();
            if (!_employees.Any() || start >= end)
                return assignments;

            // Kopie und initiale Sortierung basierend auf zuletzt geleisteten Feiertagsdiensten
            var ordered = _employees
                .OrderBy(emp => GetLastHolidayInterval(emp))
                .ToList();

            DateTime periodStart = start;

            while (periodStart < end)
            {
                DateTime periodEnd = periodStart.AddDays(_rotationPeriodDays);
                if (periodEnd > end) periodEnd = end;

                var employee = GetNextAvailableEmployee(ordered, periodStart, periodEnd);
                if (employee == null)
                {
                    MessageBox.Show(
                        "Ab dem " + periodStart.ToShortDateString() +" steht kein freier Mitarbeiter zur Verfügung.\nBitte prüfen Sie die Urlaubstage",
                        "Fehler beim Erstellen des Bereitschaftsplan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
                }

                assignments.Add(new ShiftAssignment
                {
                    EmployeeId = employee.Id,
                    EmployeeFirstName = employee.FirstName,
                    EmployeeLastName = employee.LastName,  
                    Start = periodStart,
                    End = periodEnd
                });

                // Rotation: akt. MA ans Ende
                ordered.Remove(employee);
                ordered.Add(employee);

                periodStart = periodEnd;
            }

            return assignments;
        }

        // Ermittle Abstand seit letztem Feiertagsdienst in Jahren
        private int GetLastHolidayInterval(Employee emp)
        {
            if (emp == null || !emp.LastOnHolidayYear.Any())
                return int.MaxValue;

            int lastYear = emp.LastOnHolidayYear.Values.Min();
            return DateTime.Now.Year - lastYear;
        }

        // Ermittelt wie viele Perioden seit letztem Feiertagsdienst vergangen sind
        private int GetLastHolidaySequence(Employee emp)
        {
            if (!emp.LastOnHolidayYear.Any()) return 0;
            // Nehme minimalstes Jahr (ältester Dienst)
            return DateTime.Now.Year - emp.LastOnHolidayYear.Values.Min();
        }


        // Findet den nächsten verfügbaren MA, der nicht im Urlaub ist und nicht schon Dienst
        private Employee GetNextAvailableEmployee(List<Employee> ordered, DateTime periodStart, DateTime periodEnd)
        {
            foreach (var emp in ordered)
            {
                // Urlaub prüfen
                if (emp.Vacations.Any(v => v.StartDate < periodEnd && v.EndDate >= periodStart))
                    continue;

                // Feiertagsprüfung: Wenn Zeitraum einen Feiertag enthält, darf MA dort nicht zwei Jahre Folge Dienst haben
                var holidaysInPeriod = _holidays.Where(h => h >= periodStart && h < periodEnd).ToList();
                bool holidayConflict = false;
                foreach (var h in holidaysInPeriod)
                {
                    var ht = (HolidayType)Enum.Parse(typeof(HolidayType), h.DayOfYear.ToString()); // Platzhalter: Mapping nötig
                    if (emp.LastOnHolidayYear.TryGetValue(ht, out int lastYear) && lastYear == h.Year - 1)
                    {
                        holidayConflict = true;
                        break;
                    }
                }
                if (holidayConflict) continue;

                return emp;
            }
            return null;
        }
    }

    /// <summary>
    /// Repräsentiert eine Schichtzuweisung
    /// </summary>
    public class ShiftAssignment
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
    // Demo-Konsolenprogramm
    class Program
    {
        static void Main(string[] args)
        {
            // Beispielinitialisierung (Employee-Liste, Feiertage generieren, Rotation)
            var employees = new EmployeeManager("employees.dat", Encoding.UTF8.GetBytes("16ByteSecretKey!")).LoadAll();
            var holidays = HolidayCalculator.GetHolidays(2025); // Du musst eine Feiertags-Bibliothek oder eigene Logik implementieren

            var planner = new ShiftPlanner(employees, holidays, rotationPeriodDays: 7);
            var plan = planner.GeneratePlan(new DateTime(2025, 1, 1), new DateTime(2026, 1, 1));

            // Ausgabe
            foreach (var a in plan)
                Console.WriteLine($"{a.Start:dd.MM.yyyy} - {a.End:dd.MM.yyyy}: {a.EmployeeId}");
        }
    }
    */
    /// <summary>
    /// Platzhalter für Feiertagsberechnung
    /// </summary>
    public static class HolidayCalculator
    {
        public static List<DateTime> GetHolidays(int year)
        {
            List<DateTime> holidays = new List<DateTime>
            {
                // Feste Feiertage
                new DateTime(year, 1, 1),   // Neujahr
                new DateTime(year, 5, 1),   // Tag der Arbeit
                new DateTime(year, 10, 3),  // Tag der Deutschen Einheit
                new DateTime(year, 12, 25), // 1. Weihnachtsfeiertag
                new DateTime(year, 12, 26) // 2. Weihnachtsfeiertag
            };

            // Bewegliche Feiertage basierend auf Ostersonntag
            DateTime easterSunday = GetEasterSunday(year);

            holidays.Add(easterSunday);                                      // Ostersonntag (nicht überall gesetzlich)
            holidays.Add(easterSunday.AddDays(-2));                          // Karfreitag
            holidays.Add(easterSunday.AddDays(1));                           // Ostermontag
            holidays.Add(easterSunday.AddDays(39));                          // Christi Himmelfahrt
            holidays.Add(easterSunday.AddDays(49));                          // Pfingstsonntag (nicht überall gesetzlich)
            holidays.Add(easterSunday.AddDays(50));                          // Pfingstmontag
            holidays.Add(easterSunday.AddDays(60));                          // Fronleichnam (nicht in allen Bundesländern)

            // Optional: Regionale Feiertage können je nach Bundesland ergänzt werden

            return holidays;
        }

        private static DateTime GetEasterSunday(int year)
        {
            // Berechnung nach der "Meeus/Jones/Butcher"-Formel
            int a = year % 19;
            int b = year / 100;
            int c = year % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;
            int month = (h + l - 7 * m + 114) / 31;
            int day = ((h + l - 7 * m + 114) % 31) + 1;

            return new DateTime(year, month, day);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bereitschaftsplaner
{


    internal static class Program
    {
        //Datastorage
       
        public class Global
        {
            //Daten Speicherorte initialisieren
            public static int selected_year = DateTime.Now.Year;
            public static string defaultDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), //default im AppData
                "BereitschaftsPlaner",
                "Data"
            );
            public static string employeeDataPath = Path.Combine(defaultDirectory, "Team.team");          
            public static byte[] globalKEY = Encoding.UTF8.GetBytes("16ByteSecretKey!");
            public static string logPath = Path.Combine(AppPaths.Logs, $"log_{DateTime.Now:yyyy-MM-dd}.txt");
            public static string planOutputPath = Path.Combine(AppPaths.Plans, $"Bereitschaftsplan_{selected_year}.xlsx");

        }


        // Mitarbeiter
        public class Employee
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            public string FirstName { get; set; }
            public string LastName { get; set; }
            
            // Liste der geplanten Urlaube
            public List<VacationPeriod> Vacations { get; set; } = new List<VacationPeriod>();

            // Letztes Jahr Bereitschaft an einem bestimmten Feiertag
            public Dictionary<HolidayType, int> LastOnHolidayYear { get; set; } = new Dictionary<HolidayType, int>();

            public void AddVacation(DateTime start, DateTime end)
            {
                if (end.Date < start.Date)
                    Console.WriteLine("Enddatum darf nicht vor dem Startdatum liegen.");

                Vacations.Add(new VacationPeriod { StartDate = start, EndDate = end });
            }

            public void UpdateLastHolidayYear(HolidayType holiday, int year)
            {
                if (LastOnHolidayYear.ContainsKey(holiday))
                    LastOnHolidayYear[holiday] = year;
                else
                    LastOnHolidayYear.Add(holiday, year);
            }
        }

        // Urlaubszeitraum
        public class VacationPeriod
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        // Feiertage
        public enum HolidayType
        {
            Neujahr,
            Karfreitag,
            Ostern,
            TagDerArbeit,
            ChristiHimmelfahrt,
            Pfingsten,
            TagDerDeutschenEinheit,
            Weihnachten,
            ZweiterWeihnachtstag,
            Silvester
        }


        // Repository für verschlüsselte lokale Speicherung
        public class EncryptedRepository<T> where T : class
        {
            private readonly string _filePath;
            private readonly byte[] _encryptionKey;

            public EncryptedRepository(string filePath, byte[] encryptionKey)
            {
                _filePath = filePath;
                _encryptionKey = encryptionKey;
            }

            public void Save(List<T> items)
            {
                var json = JsonConvert.SerializeObject(items, Formatting.Indented);
                var encryptedData = Protect(Encoding.UTF8.GetBytes(json));
                File.WriteAllBytes(_filePath, encryptedData);
            }

            public List<T> Load()
            {
                if (!File.Exists(_filePath))
                    return new List<T>();

                var encryptedData = File.ReadAllBytes(_filePath);
                var decryptedData = Unprotect(encryptedData);
                var json = Encoding.UTF8.GetString(decryptedData);
                return JsonConvert.DeserializeObject<List<T>>(json);
            }

            private byte[] Protect(byte[] data)
            {
                // Windows-DPAPI für Verschlüsselung
                return ProtectedData.Protect(data, _encryptionKey, DataProtectionScope.CurrentUser);
            }

            private byte[] Unprotect(byte[] encryptedData)
            {
                return ProtectedData.Unprotect(encryptedData, _encryptionKey, DataProtectionScope.CurrentUser);
            }
        }

        public class EmployeeManager
        {
            private readonly EncryptedRepository<Employee> _repo;

            public EmployeeManager(string filePath, byte[] encryptionKey)
            {
                _repo = new EncryptedRepository<Employee>(filePath, encryptionKey);
            }

            //Initialisiert Neue Liste
            public void InitializeNewTeam(string newpath)
            {
                Global.employeeDataPath = newpath;
                List<Employee> list = new List<Employee>();
                list.Clear();
                _repo.Save(list);
            }

            /// Lädt alle Mitarbeiter.
            public List<Employee> LoadAll()
            {
                return _repo.Load();
            }

            /// Speichert oder aktualisiert einen einzelnen Mitarbeiter.
            public void SaveOrUpdateEmployee(Employee employee)
            {
                var employees = _repo.Load();
                var index = employees.FindIndex(e => e.Id == employee.Id);
                if (index >= 0)
                    employees[index] = employee;
                else
                    employees.Add(employee);

                _repo.Save(employees);
            }

            /// Löscht einen Mitarbeiter anhand seiner GUID.
            public void DeleteEmployee(Guid employeeId)
            {
                var employees = _repo.Load();
                employees.RemoveAll(e => e.Id == employeeId);
                _repo.Save(employees);
            }

            /// Prüft, ob eine Mitarbeiter-GUID existiert.
            public bool Exists(Guid employeeId)
            {
                var list = _repo.Load();
                return list.Exists(emp => emp.Id == employeeId);
            }
        }


        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main_Form());

            //byte[] key = Encoding.UTF8.GetBytes("16ByteSecretKey!");
            //var manager = new EmployeeManager("employees.dat", key);

            // Laden aller Mitarbeiter
            //var employees = manager.LoadAll();


            Console.WriteLine("Mitarbeiter-Daten gespeichert und verschlüsselt.");
        }
    }
}

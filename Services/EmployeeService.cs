using edu.Models;
using Newtonsoft.Json;

namespace edu.Services
{
    public class EmployeeService
    {
        private readonly static string filePath =
            Path.Combine(Directory.GetCurrentDirectory(), "employees.json");

        public static List<Employee> ReadEmployees()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var employees = JsonConvert.DeserializeObject<List<Employee>>(json);
                return employees ?? [];
            }
            return new List<Employee>();
        }


        public static void WriteEmployees(List<Employee> employees)
        {
            var intojson =
                JsonConvert.SerializeObject(
                    employees, Newtonsoft.Json.Formatting.Indented
                );
            File.WriteAllText(filePath, intojson);
        }

        public static void AddEmployee(Employee employee)
        {
            var employees = ReadEmployees();

            employees.Add(employee);

            WriteEmployees(employees);
        }


        static public void DeleteEmployee(int employeeId)
        {
            var employees = ReadEmployees();

            foreach (var employee in employees)
            {
                if (employee.EmployeeID == employeeId)
                {
                    employees.Remove(employee);
                    WriteEmployees(employees);
                    break;
                }
            }
                    

        }


        public static void UpdateEmployeeDesignation(int employeeId, string newDesignation)
        {
            var employees = ReadEmployees();

            foreach (var employee in employees)
            {
                if (employee.EmployeeID == employeeId)
                {
                    employee.Designation = newDesignation;

                    WriteEmployees(employees);
                    return;
                }
            }
        }


        static public List<Employee> GetEmployeesByLanguage(string languageName, int score)
        {
            var employees = ReadEmployees();
            var empsWithLang = new List<Employee>();
           

            foreach (var employee in employees)
            {
                foreach (var language in employee.KnownLanguages)
                {
                    if (language.LanguageName.ToLower() == languageName.ToLower() && language.ScoreOutof100 > score)
                    {
                        empsWithLang.Add(employee);
                        break;
                    }
                }
            }

            return empsWithLang;
        }
        static public Employee? SearchById(int employeeId)
        {
            var employees = ReadEmployees();
            foreach (var Employee in employees)
            {
                if (Employee.EmployeeID == employeeId)
                {
                    return Employee;
                }
            }
            return null;
        }

    }
}
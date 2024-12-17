using edu.Models;
using Microsoft.AspNetCore.Mvc;
using edu.Services;

namespace edu.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee(Employee emp)
        {
            if (ModelState.IsValid && emp.KnownLanguages.Count>0)
            {
                EmployeeService.AddEmployee(emp); 
                return RedirectToAction(nameof(Index));
            }

            return View(emp);
        }

        [HttpGet]
        public IActionResult DisplayEmployees()
        {
            var employees = EmployeeService.ReadEmployees();
            return View(employees);
        }
        [HttpGet]
        public IActionResult EditEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EditEmployee(int employeeId, string newDesignation)
        {
            EmployeeService.UpdateEmployeeDesignation(employeeId, newDesignation);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult SearchEmployee()
        {
            return View(new List<Employee>());
        }
        [HttpGet]
        public IActionResult SearchEmployeeByID(int employeeId)
        {
            var employee = EmployeeService.SearchById(employeeId);

            List<Employee> employees = new List<Employee>();

            if (employee != null)
            {
                employees.Add(employee);
            }

            return View("SearchEmployee", employees);
        }

        [HttpGet]
        public IActionResult SearchByLanguage(String language,int score)
        {
            var employees = EmployeeService.GetEmployeesByLanguage(language, score);

            if (employees != null)
            {
                return View("SearchEmployee", employees);
            }
            return View("SearchEmployee");
        }
        [HttpGet]
        public IActionResult DeleteEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteEmployee(int employeeId)
        {
            EmployeeService.DeleteEmployee(employeeId);
            return RedirectToAction("Index");
        }



    }
}

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
        [HttpGet]
        public IActionResult AddEmployee(string msg)
        {
            return View(msg);
            
         
        }
        [HttpPost]
        public IActionResult AddEmployee(Employee emp)
        {
            string msg;
            if (ModelState.IsValid)
            {
                if (EmployeeService.AddEmployee(emp))
                {

                    msg = "student added successfully";
                    return View("AddEmployee", msg);
                }
            }
            msg = "student ID already exists";
            return View("AddEmployee", msg);
        }

        [HttpGet]
        public IActionResult DisplayEmployees(Employee emp)
        {
            if (emp.EmployeeID !=0)
            {
                List<Employee> employees = new List<Employee>();
                employees.Add(emp);
                return View(employees);
            }
            else
            {
                var employees = EmployeeService.ReadEmployees();
                return View(employees);
            }
        }
        [HttpGet]
        public IActionResult EditEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EditEmployee(int employeeId, string newDesignation)
        {
            List<Employee> employees = new List<Employee>();
            employees.Add(EmployeeService.UpdateEmployeeDesignation(employeeId, newDesignation));
            return View("DisplayEmployees",employees);
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

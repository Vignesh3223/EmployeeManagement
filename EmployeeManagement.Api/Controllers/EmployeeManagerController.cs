using EmployeeManagement.Business;
using EmployeeManagement.Persistence.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeManagerController : ControllerBase
    {
        private readonly EmployeeManager _employeeManager;

        public EmployeeManagerController(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetEmployees()
        {
            var response = _employeeManager.GetEmployees();
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetEmployeeById(int Id)
        {
            var response = _employeeManager.GetEmployeeById(Id);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateEmployee(EmployeeMaster employee)
        {
            employee.CreatedDate = DateTime.Now;
            _employeeManager.CreateEmployee(employee);
            var response = new Success
            {
                Status = "Success",
                Message = "Employee Added"

            };
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Employee")]
        public IActionResult EditEmployee(int id,EmployeeMaster employee)
        {
            if (id != employee.Id)
            {
                return BadRequest("Employee not found");
            }
            _employeeManager.EditEmployee(employee);
            var response = new Success
            {
                Status = "Success",
                Message = "Employee Updated"

            };
            return Ok(response);
         }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteEmployee(EmployeeMaster employee)
        {
            _employeeManager.DeleteEmployee(employee);
            var response = new Success
            {
                Status = "Success",
                Message = "Employee Deleted"

            };
            return Ok(response);
        }
    }
}

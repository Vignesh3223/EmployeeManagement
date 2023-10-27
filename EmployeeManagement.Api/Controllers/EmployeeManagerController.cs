using EmployeeManagement.Business;
using EmployeeManagement.Persistence.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeManagerController : ControllerBase
    {
        private readonly EmployeeManager _employeeManager;

        private readonly EmployeeManagementContext _context;

        public EmployeeManagerController(EmployeeManager employeeManager, EmployeeManagementContext context)
        {
            _employeeManager = employeeManager;
            _context = context;
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
         
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteEmployee(int id,EmployeeMaster employee)
        {
            if (id != employee.Id)
            {
                return BadRequest("Employee not found");
            }
            _employeeManager.DeleteEmployee(employee);
            var response = new Success
            {
                Status = "Success",
                Message = "Employee Deleted"

            };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStatus(int id, string activity)
        {
            var employee = _context.EmployeeMasters.FirstOrDefault(x => x.Id == id);
            if (activity == "online")
            {
                employee.IsActive = true;
            }
            else if(activity == "offline")
            {
                employee.IsActive = false;
            }
            else if (activity == "away")
            {
                employee.IsActive = null;
            }
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
            var response = new Success
            {
                Status = "Success",
                Message = "Status Updated"
            };
            return Ok(response);
        }
    }
}

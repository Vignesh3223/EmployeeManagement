using EmployeeManagement.Business.Models;
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
        private readonly IRepository<EmployeeMaster> _employeeMasterRepository;

        public EmployeeManagerController(IRepository<EmployeeMaster> employeeMasterRepository) 
        {
            _employeeMasterRepository = employeeMasterRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetEmployees()
        {
            var response = _employeeMasterRepository.GetAll();
            return Ok(response);

        }

        [HttpGet]
        public IActionResult GetEmployeeById(int Id)
        {
            var response = _employeeMasterRepository.GetById(Id);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateEmployee(EmployeeMaster employee)
        {
            _employeeMasterRepository.Add(employee);
            var response = new Success
            {
                Status = "Success",
                Message = "Employee Added"

            };
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        public IActionResult EditEmployee(EmployeeMaster employee)
        {
            _employeeMasterRepository.Update(employee);
            var response = new Success
            {
                Status = "Success",
                Message = "Employee Updated"

            };
            return Ok(response);
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteEmployee(EmployeeMaster employee)
        {
            _employeeMasterRepository.Delete(employee);
            var response = new Success
            {
                Status = "Success",
                Message = "Employee Deleted"

            };
            return Ok(response);
        }
    }
}

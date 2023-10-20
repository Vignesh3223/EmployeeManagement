using EmployeeManagement.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SalaryManagerController : ControllerBase
    {
        private readonly SalaryManager _salaryManager;

        public SalaryManagerController(SalaryManager salaryManager)
        {
            _salaryManager = salaryManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetSalary() 
        {
            var response = _salaryManager.GetSalary();
            return Ok(response);
        }
    }
}

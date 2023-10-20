using EmployeeManagement.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentManagerController : ControllerBase
    {
        private readonly DepartmentManager _departmentManager;

        public DepartmentManagerController(DepartmentManager departmentManager)
        {
            _departmentManager = departmentManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetDepartments()
        {
            var response = _departmentManager.GetDepartments();
            return Ok(response);
        }
    }
}

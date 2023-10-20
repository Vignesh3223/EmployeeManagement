using EmployeeManagement.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DesignationManagerController : ControllerBase
    {
        private readonly DesignationManager _designationManager;

        public DesignationManagerController(DesignationManager designationManager)
        {
            _designationManager = designationManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetDesignation()
        {
            var response = _designationManager.GetDesignation();
            return Ok(response);
        }
    }
}

using EmployeeManagement.Persistence.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

#nullable disable

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly EmployeeManagementContext _context;
        public LoginController(EmployeeManagementContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult UserLogin([FromBody] LoginModel login)
        {
            var employee = _context.EmployeeMasters.FirstOrDefault(x => x.Username == login.Username && x.Password == login.Password);
            if (employee == null)
            {
                return BadRequest(new Persistence.Models.Response
                {
                    Status = "Failed",
                    Message = "User not found",
                    Token = "Invalid"
                });
            }
            else
            {
                IActionResult token = GetToken(employee);
                if (token is OkObjectResult objectResult)
                {
                    string jwtToken = objectResult.Value.ToString();
                    var response = new Persistence.Models.Response
                    {
                        Status = "Success",
                        Message = "Login Success",
                        Token = jwtToken
                    };
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new Persistence.Models.Response
                    {
                        Status = "Failed",
                        Message = "Token Generation Failed",
                        Token = "Null"
                    });
                }
            }
        }

        [HttpPost]
        public IActionResult GetToken(EmployeeMaster employee)
        {
            var username = employee.Username;

            var userRole = "no role";

            if (username == "Administrator")
            {
                userRole = "Admin";
            }
            else if (username != "Administartor")
            {
                userRole = "Employee";
            }
            else
            {
                return BadRequest("User does not have a role.");
            }

            var key = "Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs05Dsa";
            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);
            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, employee.Username),
                new Claim(ClaimTypes.Role, userRole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(jwtToken);
        }
    }
}

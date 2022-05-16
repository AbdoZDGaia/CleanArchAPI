using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System.Security.Claims;
using System.Text;

namespace Presentation.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {

        private readonly ILoggerManager _logger;
        private readonly IServiceManager _service;

        public AuthenticationController(ILoggerManager logger, IServiceManager service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel user)
        {
            try
            {
                _logger.LogInfo($"{typeof(AuthenticationController)} requesting login for username {user.UserName}, using password {user.Password}");

                if (user is null)
                {
                    _logger.LogWarn("Invalid login request");
                    return BadRequest("Invalid login request");
                }
                var token = _service.AuthenticationService.Login(user);
                _logger.LogInfo($"Login Successful, token is: \n{token}");
                return Ok(new { Token = token });
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

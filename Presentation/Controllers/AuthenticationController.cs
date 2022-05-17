using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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
            var token = _service.AuthenticationService.Login(user);
            return Ok(new { Token = token });
        }
    }
}

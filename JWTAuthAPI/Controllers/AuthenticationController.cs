using Contracts;
using JWTAuthAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly string _issuer = Environment.GetEnvironmentVariable("JwtIssuer") ?? "https://localhost:5001";
        private readonly string _audience = Environment.GetEnvironmentVariable("JwtAudience") ?? "https://localhost:5001";
        private readonly string _signingKey = Environment.GetEnvironmentVariable("JwtSigningKey") ?? "SuperSecretKey@345";
        private readonly ILoggerManager _logger;

        public AuthenticationController(ILoggerManager logger)
        {
            _logger = logger;
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
                    return BadRequest("Invalid client request");
                }

                if (user.UserName == "admin" && user.Password == "admin")
                {
                    _logger.LogInfo($"Login successful for username {user.UserName}, using password {user.Password}");
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signingKey));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                    var tokenOptions = new JwtSecurityToken
                    (
                        issuer: _issuer,
                        audience: _audience,
                        claims: new[] { new Claim(JwtRegisteredClaimNames.Sub, user.UserName), },
                        expires: DateTime.UtcNow.AddMinutes(1),
                        signingCredentials: credentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    _logger.LogInfo($"Token issued is: {tokenString}");
                    return Ok(new { Token = tokenString });
                }
                _logger.LogWarn($"Invalid login request for user {user.UserName}, invalid credentials");
                return BadRequest("Invalid username or password");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

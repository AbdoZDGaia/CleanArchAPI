using Contracts;
using Entities.Models;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly string _issuer = Environment.GetEnvironmentVariable("JwtIssuer") ?? "https://localhost:5001";
        private readonly string _audience = Environment.GetEnvironmentVariable("JwtAudience") ?? "https://localhost:5001";
        private readonly string _signingKey = Environment.GetEnvironmentVariable("JwtSigningKey") ?? "SuperSecretKey@345";
        private readonly ILoggerManager _logger;

        public AuthenticationService(ILoggerManager loggerManager)
        {
            _logger = loggerManager;
        }

        public string Login(LoginModel user)
        {
            try
            {
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
                    return tokenString;
                }
                _logger.LogError($"Invalid login request for user {user.UserName}, invalid credentials");
                return "Invalid username or password";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return "Internal server error";
            }
        }
    }
}

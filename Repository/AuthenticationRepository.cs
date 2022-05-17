using Contracts;
using Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly string _issuer = Environment.GetEnvironmentVariable("JwtIssuer") ?? "https://localhost:5001";
        private readonly string _audience = Environment.GetEnvironmentVariable("JwtAudience") ?? "https://localhost:5001";
        private readonly string _signingKey = Environment.GetEnvironmentVariable("JwtSigningKey") ?? "SuperSecretKey@345";

        public string Authenticate(LoginModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signingKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken
            (
                issuer: _issuer,
                audience: _audience,
                claims: new[] { new Claim(JwtRegisteredClaimNames.Sub, user.UserName!), },
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: credentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }

        public bool userExists(LoginModel user)
        {
            return user.UserName == "admin" && user.Password == "admin";
        }
    }
}

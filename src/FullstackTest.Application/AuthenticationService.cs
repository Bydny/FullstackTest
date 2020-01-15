using FullstackTest.Application.Abstractions.Services;
using FullstackTest.Application.Authentication;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FullstackTest.Application
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService userService;
        private readonly TokenManagement tokenManagement;

        public AuthenticationService(IUserService userService, TokenManagement tokenManagement)
        {
            this.userService = userService;
            this.tokenManagement = tokenManagement;
        }

        public async Task<string> GenerateTokenAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is required");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password is required");
            }

            var user = await userService.AuthenticateUserAsync(email, password);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
            };
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                tokenManagement.Issuer,
                tokenManagement.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(tokenManagement.AccessExpiration),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return $"Bearer {token}";
        }
    }
}

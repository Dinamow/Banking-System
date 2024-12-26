using Banking_System.Application.Auth.DTOs;
using Banking_System.Application.Auth.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Banking_System.Application.Auth.UseCases
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return new RegisterResponseDTO
                {
                    Success = false,
                    Errors = new List<string> { "Email is required for registration." }
                };
            }

            var user = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return new RegisterResponseDTO
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }
            return new RegisterResponseDTO
            {
                Success = true
            };
        }

        public async Task<AuthResponseDTO> AuthenticateAsync(AuthRequestDTO request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var token = GenerateJwtToken(user);
            return new AuthResponseDTO
            {
                Token = token,
                Username = user.UserName ?? string.Empty,
            };
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is missing in appsettings.json")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

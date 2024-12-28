using Banking_System.Application.Account.UseCases;
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
        private readonly GenerateJwtTokenUseCase _generateJwtTokenUseCase;

        public AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration, GenerateJwtTokenUseCase generateJwtTokenUseCase)
        {
            _userManager = userManager;
            _generateJwtTokenUseCase = generateJwtTokenUseCase;
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
            return _generateJwtTokenUseCase.Generate(user);
        }
    }
}

using Banking_System.Application.Auth.DTOs;

namespace Banking_System.Application.Auth.Interfaces
{
    public interface IAuthService
    {
        // for login
        Task<AuthResponseDTO> AuthenticateAsync(AuthRequestDTO request);
        // for registration
        Task<RegisterResponseDTO> RegisterAsync(AuthRequestDTO request);
    }
}

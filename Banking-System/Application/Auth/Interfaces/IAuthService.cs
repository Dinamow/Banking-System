using Banking_System.Application.Auth.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Banking_System.Application.Auth.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates a user based on the provided credentials.
        /// </summary>
        /// <param name="request">The authentication request containing username and password.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the authentication response with a token and expiration date.</returns>
        Task<AuthResponseDTO> AuthenticateAsync(AuthRequestDTO request);
        /// <summary>
        /// Registers a new user with the provided credentials.
        /// </summary>
        /// <param name="request">The registration request containing username, password, and optionally email.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the registration response indicating success or failure and any errors.</returns>
        Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO request);
    }
}

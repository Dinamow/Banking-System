using Banking_System.Application.Auth.DTOs;
using Banking_System.Application.Auth.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    /// <summary>
    /// Register a new user.
    /// </summary>
    /// <remarks>
    /// This endpoint allows users to register a new account.
    /// </remarks>
    /// <response code="200"> Returns a JSON object with a success message.</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterResponseDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(AuthRequestDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _authService.RegisterAsync(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}

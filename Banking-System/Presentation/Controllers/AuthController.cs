using AutoMapper;
using Banking_System.Application.Auth.DTOs;
using Banking_System.Application.Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService, IMapper mapper)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Register a new user.
    /// </summary>
    /// <remarks>
    /// This endpoint allows users to register a new account.
    /// </remarks>
    /// <response code="201"> Returns a JSON object with a success message.</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RegisterResponseDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterRequestDTO request)
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
        return CreatedAtAction(nameof(Login), response);
    }

    /// <summary>
    /// Login a user.
    /// </summary>
    /// <remarks>
    /// this endpoint allows users to login to their account.
    /// </remarks>
    /// <response code="200">Returns a JSON object with a success message.</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResponseDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromForm] AuthRequestDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var response = await _authService.AuthenticateAsync(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

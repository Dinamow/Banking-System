using AutoMapper;
using Banking_System.Application.Account.DTOs;
using Banking_System.Application.Account.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Banking_System.Presentation.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccount(int id)
        {
            if (!await _accountService.AccountExistsAsync(id))
                return NotFound("Account not found.");

            var account = await _accountService.GetAccountAsync(id);
            var accountDTO = _mapper.Map<AccountDTO>(account);
            return Ok(accountDTO);
        }
        /// <summary>
        /// Create a new account.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="type"></param>
        /// <returns>
        /// The newly created account.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AccountDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAccount([FromForm] string type)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User not authenticated.");

            try
            {
                var account = await _accountService.CreateAccountAsync(userId, type);
                var accountDTO = _mapper.Map<AccountDTO>(account);
                return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, accountDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Get the balance of an account.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// The balance of the account.
        /// </returns>
        [HttpGet("{id}/balance")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(decimal))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccountBalance(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                var account = await _accountService.GetMyAccountAsync(userId, id);
                return Ok(account.Balance);
            }
            catch
            {
                return NotFound("Account not found.");
            }
        }
    }
}

using AutoMapper;
using Banking_System.Application.Account.DTOs;
using Banking_System.Application.Account.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Banking_System.Presentation.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accService;
        private readonly IMapper _mapper;
        public AccountController(IAccountService accService, IMapper mapper)
        {
            _accService = accService;
            _mapper = mapper;
        }

        [HttpGet("accounts/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccount(int id)
        {
            if (!await _accService.AccountExistsAsync(id))
                return NotFound();
            var account = await _accService.GetAccountAsync(id);
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
        [HttpPost("accounts/")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AccountDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAccount([FromForm] string type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (string.IsNullOrEmpty(type))
            {
                return BadRequest("Account type is required.");
            }
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine(UserId);
            if (string.IsNullOrEmpty(UserId))
            {
                return BadRequest("User not found");
            }
            try
            {
                var account = await _accService.CreateAccountAsync(UserId, type);
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
        [HttpGet("accounts/{id}/Balance")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAccountBalance(int id)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                var account = await _accService.GetMyAccountAsync(UserId, id);
                return Ok(account.Balance);
            }
            catch (Exception ex)
            {
                return NotFound("You Don't have account with this ID");

            }
        }
    }
}

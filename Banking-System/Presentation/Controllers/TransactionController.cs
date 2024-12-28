using AutoMapper;
using Banking_System.Application.Transaction.DTOs;
using Banking_System.Application.Transaction.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Banking_System.Presentation.Controllers
{
    [Route("api/account/")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly IBankingTransaction _transaction;
        private readonly IMapper _mapper;
        public TransactionController(IBankingTransaction transaction, IMapper mapper)
        {
            _transaction = transaction;
            _mapper = mapper;
        }
        /// <summary>
        /// Deposit money into an account.
        /// </summary>
        /// <param name="depositDTO">
        /// The deposit data transfer object.
        /// </param>
        /// <returns>
        /// A JSON object with the transaction details.
        /// </returns>
        [HttpPost]
        [Route("deposit")]
        [ProducesResponseType(200, Type = typeof(TransactionDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Deposit([FromForm] DepositDTO depositDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var transaction = await _transaction.CreateTransactionAsync(string.Empty, depositDTO.Id, "deposit", depositDTO.Amount, 0);
                var transactionDTO = _mapper.Map<TransactionDTO>(transaction);
                return Ok(transactionDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Withdraw money from an account.
        /// </summary>
        /// <param name="withdrawDTO">
        /// The deposit data transfer object.
        /// </param>
        /// <returns>
        /// A JSON object with the transaction details.
        /// </returns>
        [HttpPost]
        [Route("withdraw")]
        [ProducesResponseType(200, Type = typeof(TransactionDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Withdraw([FromForm] DepositDTO withdrawDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var transaction = await _transaction.CreateTransactionAsync(UserId, withdrawDTO.Id, "withdraw", withdrawDTO.Amount, 0);
                var transactionDTO = _mapper.Map<TransactionDTO>(transaction);
                return Ok(transactionDTO);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        /// <summary>
        /// Transfer money from acccount to acccount
        /// </summary>
        /// <param name="transDTO">
        /// The transfer data transfer object.
        /// </param>
        /// <returns>
        /// A JSON object with the transaction details.
        /// </returns>
        [HttpPost]
        [Route("transfer")]
        [ProducesResponseType(200, Type = typeof(TransactionDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Transfer([FromForm] TransferDTO transDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var transaction = await _transaction.CreateTransactionAsync(UserId, transDTO.FromAccount, "transfer", transDTO.Amount, transDTO.ToAccount);
                var transactionDTO = _mapper.Map<TransactionDTO>(transaction);
                return Ok(transactionDTO);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}

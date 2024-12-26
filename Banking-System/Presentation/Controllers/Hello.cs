using BankingSystem.Application.Account.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Banking_System.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    [Authorize]
    public class Hello : ControllerBase
    {
        /// <summary>
        /// Returns a hello world message.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a JSON object containing a simple "Hello, World!" message. 
        /// It can be used to verify that the API is working as expected.
        /// </remarks>
        /// <response code="200">Returns a JSON object with a hello world message.</response>
        [HttpGet("hello")]
        [ProducesResponseType(typeof(HelloWorldResponse), 200)]
        public IActionResult GetHello()
        {
            var response = new HelloWorldResponse
            {
                Hello = "World!"
            };
            //print the user usernmae
            var user = User.FindFirst(ClaimTypes.Name)?.Value;
            Console.WriteLine(user);
            return Ok(response);
        }
    }
}

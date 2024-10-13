using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TGLabAPI.Application.DTOs.Bet.Response;
using TGLabAPI.Application.DTOs.Common;
using TGLabAPI.Application.Interfaces.Services.Transaction;

namespace TGLabAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("list")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PageableResponse<GetTransactionResponse>>> List([FromQuery] PageableRequest pagination)
        {
            try
            {
                var result = await _transactionService.List(pagination);

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}

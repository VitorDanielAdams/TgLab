using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TGLabAPI.Application.DTOs.Common;
using TGLabAPI.Application.DTOs.Transaction.Request;
using TGLabAPI.Application.DTOs.Transaction.Response;
using TGLabAPI.Application.Interfaces.Services.Transaction;

namespace TGLabAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/bet")]
    public class BetController : ControllerBase
    {
        private readonly IBetService _betService;
        public BetController(IBetService betService)
        {
            _betService = betService;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetBetResponse>> CreateBet([FromBody] CreateBetRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _betService.CreateBet(request);

                return StatusCode(StatusCodes.Status201Created, result);
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

        [HttpPost("{id}/cancel")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetBetResponse>> CancelBet(Guid id)
        {
            try
            {
                var result = await _betService.CancelBet(id);

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

        [HttpGet("list")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PageableResponse<GetBetListDetailResponse>>> List([FromQuery] PageableRequest pagination)
        {
            try
            {
                var result = await _betService.List(pagination);

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

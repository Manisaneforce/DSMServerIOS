using DSMServerMani.Models;
using DSMServerMani.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSMServerMani.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckinController : ControllerBase
    {
        private readonly ICheckinService _service;

        public CheckinController(ICheckinService service)
        {
            _service = service;
        }

        #region Checkin

        [HttpPost("checkin")]
        public async Task<IActionResult> CheckinAsync([FromBody] CheckinRequestModel checkinRequestModel)
        {
            try
            {
                if (checkinRequestModel == null)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Invalid request. Please provide check-in details."
                    });
                }

                var result = await _service.UserCheckinVerification(checkinRequestModel);

                if (result == null)
                {
                    return NotFound(new
                    {
                        Success = false,
                        Message = "Check-in failed or user not found."
                    });
                }

                return Ok(new
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Error = ex.Message
                });
            }
        }

        #endregion
    }
}

using DSMServerMani.Models;
using DSMServerMani.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DSMServerMani.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginService _service;


        public LoginController(ILoginService service)
        {
            _service = service;
        }

        #region Login

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel loginRequestModel)
        {
            try
            {
                if (loginRequestModel == null)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Invalid request. Please provide login details."
                    });
                }

                var result = await _service.UserLoginVerification(loginRequestModel);
                    return Ok(result);
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

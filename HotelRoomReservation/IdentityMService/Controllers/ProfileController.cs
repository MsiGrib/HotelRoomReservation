using DataModel.ModelsResponse;
using IdentityMService.ModelsRR;
using IdentityMService.Service;
using IdentityMService.Utilitys;
using IdentityMService.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserProfileService _userProfileService;

        public ProfileController(ILogger<AuthController> logger, IUserProfileService userProfileService)
        {
            _logger = logger;
            _userProfileService = userProfileService;
        }

        /// <summary>
        /// For Client
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ProfileUser")]
        public async Task<IActionResult> GetProfileUser()
        {
            try
            {
                var userIdClaim = User.FindFirst("userId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userGuid))
                {
                    return BadRequest(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Message = "Не прошли авторизацию!"
                    });
                }

                var userProfile = _userProfileService.GetUserProfileByUserIdAsync(userGuid)

                return Ok();
            }
            catch (Exception)
            {
                return new ObjectResult(new BaseResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Ошибка на стороне сервера!"
                });
            }
        }
    }
}

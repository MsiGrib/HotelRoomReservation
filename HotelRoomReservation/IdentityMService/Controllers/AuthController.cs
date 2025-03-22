using IdentityMService.ModelsRR;
using IdentityMService.Service;
using IdentityMService.Utilitys;
using IdentityMService.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IdentityMService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;
        private readonly BasicConfiguration _basicConfiguration;

        public AuthController(ILogger<AuthController> logger, IUserService userService, BasicConfiguration basicConfiguration)
        {
            _logger = logger;
            _userService = userService;
            _basicConfiguration = basicConfiguration;
        }

        [HttpPost(Name = "Authorization")]
        public async Task<IActionResult> AuthorizationUser([FromBody] AuthorizationRequest request)
        {
            try
            {
                if (!AuthCValidator.IsValidAuthorizationRequest(request))
                {
                    return BadRequest();
                }

                var user = await _userService.AuthorizationUserAsync(request.Login, request.Password);

                if (user == null)
                {
                    return Unauthorized();
                }

                string token = TokenUtility.CreateJWTToken(_basicConfiguration.SecretJWT, _basicConfiguration.IssuerJWT, _basicConfiguration.AudienceJWT, user.Id.ToString(), DateTime.Now.AddHours(3));

                var response = new AuthorizationResponse
                {
                    ExpirationTimeToken = DateTime.Now.AddHours(3),
                    Token = token,
                };

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost(Name = "Registration")]
        public async Task<IActionResult> RegistrationUser([FromBody] RegistrationRequest request)
        {
            try
            {
                if (!AuthCValidator.IsValidRegistrationRequest(request))
                {
                    return BadRequest();
                }

                bool status = await _userService.RegistrationUserAsync(request.Login, request.Password, request.NumberPhone, request.LastName, request.FirstName, request.Email);

                if (!status)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

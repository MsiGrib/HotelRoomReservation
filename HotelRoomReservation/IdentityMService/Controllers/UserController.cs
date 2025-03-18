using IdentityMService.ModelsRR;
using IdentityMService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityMService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly IUserService _userService;
        private readonly BasicConfiguration _basicConfiguration;

        public UserController(ILogger<TokenController> logger, IUserService userService, BasicConfiguration basicConfiguration)
        {
            _logger = logger;
            _userService = userService;
            _basicConfiguration = basicConfiguration;
        }

        [HttpPost(Name = "Create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.NumberPhone)
                || string.IsNullOrEmpty(request.LastName) || string.IsNullOrEmpty(request.FirstName)
                || string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest();
            }

            await _userService.AddUserAsync(request.Login, request.Password, request.NumberPhone,
                request.LastName, request.FirstName, request.Email);

            return Ok();
        }
    }
}

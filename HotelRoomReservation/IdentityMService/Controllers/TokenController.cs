using IdentityMService.ModelsRR;
using IdentityMService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly IUserService _userService;

        public TokenController(ILogger<TokenController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost(Name = "Create")]
        public IActionResult CreateToken([FromBody] TokenRequest request)
        {
            if (string.IsNullOrEmpty(request.UserLogin) || string.IsNullOrEmpty(request.UserEmail))
            {
                return BadRequest();
            }






        }


    }
}

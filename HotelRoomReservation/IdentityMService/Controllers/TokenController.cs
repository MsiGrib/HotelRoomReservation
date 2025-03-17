using IdentityMService.ModelsRR;
using IdentityMService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace IdentityMService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly IUserService _userService;
        private readonly BasicConfiguration _basicConfiguration;

        public TokenController(ILogger<TokenController> logger, IUserService userService, BasicConfiguration basicConfiguration)
        {
            _logger = logger;
            _userService = userService;
            _basicConfiguration = basicConfiguration;
        }

        [HttpGet(Name = "CreateInitial")]
        public IActionResult CreateInitialToken()
        {
            string secretKey = _basicConfiguration.SecretJWT;
            string issuer = _basicConfiguration.IssuerJWT;
            string audience = _basicConfiguration.AudienceJWT;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: null,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                token = jwt
            });
        }

        [HttpPost(Name = "CreateMain")]
        public IActionResult CreateMainToken([FromBody] TokenRequest request)
        {
            if (string.IsNullOrEmpty(request.UserEmail))
            {
                return BadRequest();
            }

            string secretKey = _basicConfiguration.SecretJWT;
            string issuer = _basicConfiguration.IssuerJWT;
            string audience = _basicConfiguration.AudienceJWT;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var user = _userService.GetUserByEmailAsync(request.UserEmail);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "User not found.");
            }

            var claims = new[]
            {
                new Claim("userId", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(12),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                token = jwt
            });
        }
    }
}

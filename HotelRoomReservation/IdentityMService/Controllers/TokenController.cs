using IdentityMService.ModelsRR;
using IdentityMService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

        [HttpPost(Name = "Create")]
        public async Task<IActionResult> CreateToken([FromBody] CreateTokenRequest request)
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

            var user = await _userService.GetUserByEmailAsync(request.UserEmail);

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

            return Ok(new CreateTokenResponse
            {
                Token = jwt,
                ExpirationDate = DateTime.UtcNow.AddHours(12),
            });
        }

        [HttpPost(Name = "Refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                return BadRequest();
            }

            ClaimsPrincipal principal;
            try
            {
                principal = GetPrincipalFromExpiredToken(request.Token);
            }
            catch (SecurityTokenException)
            {
                return BadRequest();
            }

            var userIdClaim = principal.Claims.FirstOrDefault(x => x.Type == "userId");
            if (userIdClaim == null)
            {
                return BadRequest("");
            }

            var user = await _userService.GetUserByIdAsync(userIdClaim.Value);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "User not found.");
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_basicConfiguration.SecretJWT)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        private string GenerationJWTToken()
        {

        }
    }
}

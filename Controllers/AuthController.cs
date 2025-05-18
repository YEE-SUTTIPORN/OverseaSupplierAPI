using Microsoft.AspNetCore.Mvc;
using OverseaSupplierAPI.Models;
using OverseaSupplierAPI.Services;

namespace OverseaSupplierAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username == "test" && request.Password == "1234")
            {
                var tokens = _tokenService.GenerateTokens(request.Username);
                return Ok(tokens);
            }

            return Unauthorized();
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshTokenRequest obj)
        {
            var tokens = _tokenService.Refresh(obj.RefreshToken);
            if (tokens == null)
                return Unauthorized();

            return Ok(tokens);
        }

    }
}

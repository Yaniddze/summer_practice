using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApi.Controllers.Contract.Requests;
using TestApi.Services;

namespace TestApi.Controllers
{
    
    public class IdentityController: Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            this._identityService = identityService;
        }

        [HttpPost("api/register")]
        public async Task<IActionResult> register([FromBody] RegistrationRequest request)
        {
            var registerResponse = await _identityService.registerAsync(request.email, request.password);
            
            return Ok(registerResponse);
        }
        
        [HttpPost("api/login")]
        public async Task<IActionResult> login([FromBody] LoginRequest request)
        {
            var loginResponse = await _identityService.loginAsync(request.email, request.password);
            
            return Ok(loginResponse);
        }

        [HttpPost("api/refresh")]
        public async Task<IActionResult> refresh([FromBody] RefreshTokenRequest request)
        {
            var refreshResponse = await _identityService.refreshTokenAsync(request.Token, request.RefreshToken);
            
            return Ok(refreshResponse);
        }
    }
}
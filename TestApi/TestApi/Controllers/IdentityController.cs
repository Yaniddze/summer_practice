using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestApi.Controllers.Contract.Requests;
using TestApi.Services;
using TestApi.UseCases.Registration;

namespace TestApi.Controllers
{
    
    public class IdentityController: Controller
    {
        private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;

        public IdentityController(IIdentityService identityService, IMediator mediator)
        {
            this._identityService = identityService;
            _mediator = mediator;
        }

        [HttpPost("api/identity/register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }
        
        [HttpPost("api/identity/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var loginResponse = await _identityService.loginAsync(request.email, request.password);
            
            return Ok(loginResponse);
        }

        [HttpPost("api/identity/refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var refreshResponse = await _identityService.refreshTokenAsync(request.Token, request.RefreshToken);
            
            return Ok(refreshResponse);
        }
    }
}
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestApi.Controllers.Contract.Requests;
using TestApi.Controllers.Contract.Responses;
using TestApi.Services;
using TestApi.UseCases.GenerateToken;
using TestApi.UseCases.Login;
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
            var registrationResult = await _mediator.Send(request);
            var result = new RegistrationResponse
            {
                Success = registrationResult.Success,
                Errors = registrationResult.Errors,
            };

            if (!registrationResult.Success) return Ok(result);
            
            var tokens = await _mediator.Send(new GenerateTokenRequest
            {
                UserId = registrationResult.UserId,
                Email = registrationResult.Email,
            });
            result.Token = tokens.Token;
            result.RefreshToken = tokens.RefreshedToken;

            return Ok(result);
        }
        
        [HttpPost("api/identity/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var loginResult = await _mediator.Send(request);
            var result = new LoginResponse
            {
                Success = loginResult.Success,
                Errors = loginResult.Errors,
            };

            if (!loginResult.Success) return Ok(result);
            
            var tokens = await _mediator.Send(new GenerateTokenRequest
            {
                UserId = loginResult.UserId,
                Email = loginResult.Email,
            });
            result.Token = tokens.Token;
            result.RefreshToken = tokens.RefreshedToken;

            return Ok(result);
        }

        [HttpPost("api/identity/refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var refreshResponse = await _identityService.refreshTokenAsync(request.Token, request.RefreshToken);
            
            return Ok(refreshResponse);
        }
    }
}
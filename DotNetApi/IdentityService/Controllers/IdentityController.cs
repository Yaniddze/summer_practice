using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestApi.Controllers.Responses;
using TestApi.UseCases.ActivateAccount;
using TestApi.UseCases.GenerateToken;
using TestApi.UseCases.Login;
using TestApi.UseCases.RefreshToken;
using TestApi.UseCases.Registration;

namespace TestApi.Controllers
{
    [Route("api/identity")]
    public class IdentityController : Controller
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
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
                Platform = request.Platform,
            });
            result.Token = tokens.Token;
            return Ok(result);
        }

        [HttpPost("login")]
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
                Platform = request.Platform,
            });
            result.Token = tokens.Token;

            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var refreshingResult = await _mediator.Send(request);
            var result = new RefreshTokenResponse
            {
                Success = refreshingResult.Success,
                Errors = refreshingResult.Errors,
            };

            if (!refreshingResult.Success) return Ok(result);

            var tokens = await _mediator.Send(new GenerateTokenRequest
            {
                UserId = refreshingResult.UserId,
                Platform = request.Platform,
            });

            result.Token = tokens.Token;

            return Ok(result);
        }

        [HttpPost("activate")]
        public async Task<IActionResult> ActivateAccount([FromBody] ActivateRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}
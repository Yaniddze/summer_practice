using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestApi.Controllers.Responses;
using TestApi.UseCases.GenerateToken;
using TestApi.UseCases.Login;
using TestApi.UseCases.RefreshToken;
using TestApi.UseCases.Registration;
using TestApi.UseCases.SendMail;

namespace TestApi.Controllers
{
    public class IdentityController: Controller
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
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

            await _mediator.Send(new EmailRequest
            {
                Email = registrationResult.Email,
                Message = "Hello, User!",
                Subject = "Подтверждение регистрации"
            });
            
            var tokens = await _mediator.Send(new GenerateTokenRequest
            {
                UserId = registrationResult.UserId,
                Email = registrationResult.Email,
            });
            result.Token = tokens.Token;
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

            return Ok(result);
        }

        [HttpPost("api/identity/refresh")]
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
                Email = refreshingResult.Email,
            });

            result.Token = tokens.Token;

            return Ok(result);
        }
    }
}
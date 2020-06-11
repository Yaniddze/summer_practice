using System.Threading.Tasks;
using Gateway.UseCases.ActivateAccount;
using Gateway.UseCases.Login;
using Gateway.UseCases.RefreshToken;
using Gateway.UseCases.Registration;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [Route("api/v1/identity")]
    public class IdentityController: Controller
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _mediator.Send(request);
            
            return Ok(response);
        }
        
        [HttpPost("registration")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            var response = await _mediator.Send(request);
            
            return Ok(response);
        }
        
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            var response = await _mediator.Send(request);
            
            return Ok(response);
        }
        
        [HttpPost("activate")]
        public async Task<IActionResult> Activate([FromBody] ActivateRequest request)
        {
            var response = await _mediator.Send(request);
            
            return Ok(response);
        }
    }
}

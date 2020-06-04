using System.Threading.Tasks;
using AspGateway.UseCases.Login;
using AspGateway.UseCases.RefreshToken;
using AspGateway.UseCases.Registration;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AspGateway.Controllers
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
    }
}

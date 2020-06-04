using System.Threading.Tasks;
using AspGateway.UseCases.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AspGateway.Controllers
{
    [Route("api/v1/[controller]")]
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
    }
}

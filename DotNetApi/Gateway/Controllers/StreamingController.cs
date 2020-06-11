using System.IO;
using System.Threading.Tasks;
using Gateway.UseCases.AddMusic;
using Gateway.UseCases.GetMusic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [Route("api/v1/streaming/music")]
    public class StreamingController: Controller
    {
        private readonly IMediator _mediator;

        public StreamingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetMusic([FromQuery] GetMusicRequest request)
        {
            var response = await _mediator.Send(request);

            if (!response.Success)
            {
                return Ok(response);
            }
            return File(new MemoryStream(response.Music),"audio/mpeg");
        }
        
//        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddMusic([FromQuery] AddMusicRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
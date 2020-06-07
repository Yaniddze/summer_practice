using System.IO;
using System.Net;
using System.Threading.Tasks;
using AspGateway.UseCases.GetMusic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AspGateway.Controllers
{
    [Route("api/v1/streaming")]
    public class StreamingController: Controller
    {
        private readonly IMediator _mediator;

        public StreamingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetMusic([FromQuery] GetMusicRequest request)
        {
            var response = await _mediator.Send(request);

            return File(new MemoryStream(response.Music),"audio/mpeg");
        }
    }
}
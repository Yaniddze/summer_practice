using System;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StreamingService.UseCases.AddSong;
using StreamingService.UseCases.GetSong;

namespace StreamingService.Controllers
{
    [Route("api/streaming")]
    public class StreamingController: Controller
    {
        private readonly IMediator _mediator;

        public StreamingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetMusic([FromQuery] GetSongRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.Success)
            {
                return Ok(result);
            }
            
            return File(new MemoryStream(result.Content), "audio/mpeg");
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMusic([FromBody] AddSongRequest request)
        {
            if (request == null)
            {
                Console.WriteLine(Request.Body);
                return Ok(new {Success = false});
            }

            
            var result = await _mediator.Send(request);
            
            return Ok(result);
        }
    }
}
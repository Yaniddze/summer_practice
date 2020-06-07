using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StreamingApi.Controllers
{
    [Route("api/streaming")]
    public class StreamingController: Controller
    {
        [HttpGet("music")]
        public async Task<IActionResult> GetMusic([FromQuery] string musicTitle)
        {
            var music = Directory.GetCurrentDirectory() + "/Music/NoOneLikeYou.mp3";
            
            return File(new MemoryStream(System.IO.File.ReadAllBytes(music)), "audio/mpeg");
        }
    }
}
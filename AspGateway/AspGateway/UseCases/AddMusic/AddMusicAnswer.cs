using System.Collections.Generic;

namespace AspGateway.UseCases.AddMusic
{
    public class AddMusicAnswer
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
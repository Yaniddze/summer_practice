using System.Collections.Generic;

namespace Gateway.UseCases.AddMusic
{
    public class AddMusicAnswer
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
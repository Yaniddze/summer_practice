using System.Collections.Generic;

namespace StreamingService.UseCases.AddSong
{
    public class AddSongAnswer
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
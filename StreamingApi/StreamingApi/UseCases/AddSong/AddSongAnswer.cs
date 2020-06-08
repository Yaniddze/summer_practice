using System.Collections.Generic;

namespace StreamingApi.UseCases.AddSong
{
    public class AddSongAnswer
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
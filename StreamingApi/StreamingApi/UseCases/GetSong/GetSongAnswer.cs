using System.Collections.Generic;

namespace StreamingApi.UseCases.GetSong
{
    public class GetSongAnswer
    {
        public GetSongAnswer()
        {
            Errors = new List<string>();
        }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public byte[] Content { get; set; }
    }
}
using System.Collections.Generic;

namespace Gateway.UseCases.GetMusic
{
    public class GetMusicAnswer
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public byte[] Music { get; set; }
    }
}
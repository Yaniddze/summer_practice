using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace AspGateway.UseCases.GetMusic
{
    public class GetMusicAnswer
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public byte[] Music { get; set; }
    }
}
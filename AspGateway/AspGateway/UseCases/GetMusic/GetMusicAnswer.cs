using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace AspGateway.UseCases.GetMusic
{
    public class GetMusicAnswer
    {
        public byte[] Music { get; set; }
    }
}
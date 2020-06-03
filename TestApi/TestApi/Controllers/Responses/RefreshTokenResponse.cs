using System.Collections.Generic;

namespace TestApi.Controllers.Responses
{
    public class RefreshTokenResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public string Token { get; set; }
    }
}   
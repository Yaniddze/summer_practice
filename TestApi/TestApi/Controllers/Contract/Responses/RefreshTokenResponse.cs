using System.Collections.Generic;

namespace TestApi.Controllers.Contract.Responses
{
    public class RefreshTokenResponse
    {
        public List<string> errors { get; set; } = new List<string>();
        public string token { get; set; }
        public string refreshToken { get; set; }
    }
}   
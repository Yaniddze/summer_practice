using System.Collections.Generic;

namespace TestApi.Controllers.Contract.Responses
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
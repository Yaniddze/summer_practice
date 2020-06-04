using System.Collections.Generic;

namespace TestApi.Controllers.Responses
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string Token { get; set; }
    }
}
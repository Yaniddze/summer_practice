using System.Collections.Generic;

namespace TestApi.Controllers.Contract.Responses
{
    public class LoginResponse
    {
        public bool isSuccess { get; set; }
        public List<string> errors { get; set; }
        public string token { get; set; }
        public string refreshToken { get; set; }
    }
}
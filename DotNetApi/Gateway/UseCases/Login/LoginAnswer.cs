using System.Collections.Generic;

namespace Gateway.UseCases.Login
{
    public class LoginAnswer
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string Token { get; set; }
    }
}
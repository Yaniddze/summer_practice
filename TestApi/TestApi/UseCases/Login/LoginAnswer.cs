using System.Collections.Generic;

namespace TestApi.UseCases.Login
{
    public class LoginAnswer
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
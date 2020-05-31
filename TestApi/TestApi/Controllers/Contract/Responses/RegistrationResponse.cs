using System.Collections.Generic;

namespace TestApi.Controllers.Contract.Responses
{
    public class RegisterResponse
    {
        public bool isRegistered { get; set; }
        public List<string> errors { get; set; } = new List<string>();
        public string token { get; set; }
    }
}
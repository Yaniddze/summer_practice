using System.Collections.Generic;

namespace TestApi.Controllers.Contract.Responses
{
    public class RegistrationResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }   
}
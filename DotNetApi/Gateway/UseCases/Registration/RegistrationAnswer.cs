using System.Collections.Generic;

namespace Gateway.UseCases.Registration
{
    public class RegistrationAnswer
    {
        public string Token { get; set; }
        public List<string> Errors { get; set; }
        public bool Success { get; set; }
    }
}

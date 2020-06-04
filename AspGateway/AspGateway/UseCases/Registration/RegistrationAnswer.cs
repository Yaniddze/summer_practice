using System.Collections.Generic;

namespace AspGateway.UseCases.Registration
{
    public class RegistrationAnswer
    {
        public string Token { get; set; }
        public List<string> Errors { get; set; }
        public bool Success { get; set; }
    }
}

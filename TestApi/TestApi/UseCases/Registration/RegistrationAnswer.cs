using System.Collections.Generic;

namespace TestApi.UseCases.Registration
{
    public class RegistrationAnswer
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
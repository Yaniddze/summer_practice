using System.Collections.Generic;

namespace Core.UseCases.Registration
{
    public class RegistrationAnswer
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
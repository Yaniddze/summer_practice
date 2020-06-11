using System;
using System.Collections.Generic;

namespace TestApi.UseCases.Registration
{
    public class RegistrationAnswer
    {
        public RegistrationAnswer()
        {
            Errors = new List<string>();
        }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; }
        public Guid ActivationUrl { get; set; }
    }
}
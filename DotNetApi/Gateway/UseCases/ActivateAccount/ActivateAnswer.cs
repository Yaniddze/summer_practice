using System.Collections.Generic;

namespace Gateway.UseCases.ActivateAccount
{
    public class ActivateAnswer
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
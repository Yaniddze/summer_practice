using System.Collections.Generic;

namespace AspGateway.UseCases.ActivateAccount
{
    public class ActivateAnswer
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
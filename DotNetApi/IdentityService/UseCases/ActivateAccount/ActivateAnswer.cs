using System.Collections.Generic;

namespace TestApi.UseCases.ActivateAccount
{
    public class ActivateAnswer
    {
        public ActivateAnswer()
        {
            Errors = new List<string>();
        }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
using System.Collections.Generic;

namespace Gateway.UseCases.RefreshToken
{
    public class RefreshAnswer
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string Token { get; set; }
    }
}

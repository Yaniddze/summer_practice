using System;
using System.Collections.Generic;

namespace TestApi.UseCases.Login
{
    public class LoginAnswer
    {
        public LoginAnswer()
        {
            Errors = new List<string>();
        }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public Guid UserId { get; set; }
    }
}
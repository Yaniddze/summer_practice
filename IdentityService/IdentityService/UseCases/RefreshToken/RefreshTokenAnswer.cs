using System;
using System.Collections.Generic;

namespace TestApi.UseCases.RefreshToken
{
    public class RefreshTokenAnswer
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}
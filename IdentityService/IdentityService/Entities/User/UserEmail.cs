using System;

namespace TestApi.Entities.User
{
    public class UserEmail
    {
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public Guid ActivationUrl { get; set; }
    }
}
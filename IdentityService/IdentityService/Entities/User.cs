using System;

namespace TestApi.Entities
{
    public class User: Entity
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string Password { get; set; }
        public Guid ActivationUrl { get; set; }
        public UserToken UserToken { get; private set; } = new UserToken();
    }

    public class UserToken
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string JwtId { get; set; }
    }
}
using System;
using TestApi.Entities.Abstractions;

namespace TestApi.Entities.User
{
    // <summary>
    // Registration info
    // </summary>
    public class User: Entity
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        
        // <summary>
        // Email info
        // </summary>
        public UserEmail UserEmail { get; set; }
        
        // <summary>
        // Token info
        // </summary>
        public UserToken UserToken { get; private set; } = new UserToken();
    }
}

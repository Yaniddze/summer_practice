using TestApi.Entities.Abstractions;

namespace TestApi.Entities.Users
{
    // <summary>
    // Registration info
    // </summary>
    public class User: Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        
        // <summary>
        // Email info
        // </summary>
        public UserEmail UserEmail { get; set; }
    }
}

namespace Core.Entities
{
    public class User: Entity
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string Password { get; set; }
    }
}
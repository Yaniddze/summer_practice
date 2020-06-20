using System;

namespace TestApi.DataBase.Entities
{
    public class UserDB
    {
        public Guid id { get; set; }
        public string email { get; set; }
        public DateTime register_date { get; set; }
        public bool is_email_confirmed { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public Guid activation_url { get; set; }
    }
}
using System;
using TestApi.Entities;

namespace TestApi.DataBase.Entities
{
    public class UserDB
    {
        public Guid id { get; set; }
        public string login { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public bool isEmailConfirmed { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        public DateTime expiry_date { get; set; }
        public DateTime creation_date { get; set; }
        public string JwtId { get; set; }
    }
}
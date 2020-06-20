using System;

namespace TestApi.DataBase.Entities
{
    public class TokenDB
    {
        public Guid id { get; set; }
        public string token { get; set; }
        public Guid user_id { get; set; }
        public DateTime expiry_date { get; set; }
        public DateTime creation_date { get; set; }
        public string jwt_id { get; set; }
    }
}
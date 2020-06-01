using System;

namespace TestApi.Entities
{
    public class RefreshToken: Entity
    {
        public string JwtId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Guid UserId { get; set; }
    }
}
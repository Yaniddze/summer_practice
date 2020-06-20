using System;
using TestApi.Entities.Abstractions;

namespace TestApi.Entities.Tokens
{
    public class Token: Entity
    {
        public string TokenValue { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string JwtId { get; set; }
    }
}
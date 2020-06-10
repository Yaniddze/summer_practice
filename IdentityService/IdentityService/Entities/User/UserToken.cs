using System;
using System.Collections.Generic;
using TestApi.Entities.Abstractions;

namespace TestApi.Entities.User
{
    public class UserToken: ValueObject
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string JwtId { get; set; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Token;
            yield return ExpiryDate;
            yield return CreationDate;
            yield return JwtId;
        }
    }
}
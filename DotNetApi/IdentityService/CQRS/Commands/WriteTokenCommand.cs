using System;
using TestApi.CQRS.Commands.Abstractions;

namespace TestApi.CQRS.Commands
{
    public class WriteTokenCommand: ICommand
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public string Platform { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string JwtId { get; set; }
    }
}
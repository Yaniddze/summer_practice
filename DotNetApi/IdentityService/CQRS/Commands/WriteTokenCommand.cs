using System;
using TestApi.CQRS.Commands;
using TestApi.CQRS.Commands.Abstractions;
using TestApi.Entities.User;

namespace TestApi.DataBase.CQRS.Users.Commands.Update.WriteToken
{
    public class WriteTokenCommand: ICommand
    {
        public Guid UserId { get; set; }
        public UserToken NewToken { get; set; }
    }
}
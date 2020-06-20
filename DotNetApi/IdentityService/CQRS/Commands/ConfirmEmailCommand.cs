using System;
using TestApi.CQRS.Commands;
using TestApi.CQRS.Commands.Abstractions;

namespace TestApi.DataBase.CQRS.Users.Commands.Update.ConfirmEmail
{
    public class ConfirmEmailCommand: ICommand
    {
        public Guid UserId { get; set; }
    }
}
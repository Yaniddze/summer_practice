using System;
using TestApi.CQRS.Commands;

namespace TestApi.DataBase.CQRS.Users.Commands.Update.ConfirmEmail
{
    public class ConfirmEmailCommand: ICommand
    {
        public Guid UserId { get; set; }
    }
}
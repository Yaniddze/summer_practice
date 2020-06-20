using TestApi.CQRS.Commands;
using TestApi.CQRS.Commands.Abstractions;
using TestApi.Entities.User;

namespace TestApi.DataBase.CQRS.Users.Commands.Add
{
    public class AddUserCommand: ICommand
    {
        public User UserToAdd { get; set; }
    }
}
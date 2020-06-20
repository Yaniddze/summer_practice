using TestApi.CQRS.Commands.Abstractions;
using TestApi.Entities.Users;

namespace TestApi.CQRS.Commands
{
    public class AddUserCommand: ICommand
    {
        public User UserToAdd { get; set; }
    }
}
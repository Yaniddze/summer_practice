using System.Threading.Tasks;

namespace TestApi.CQRS.Commands
{
    public interface ICommandHandler<TCommand> 
        where TCommand: ICommand
    {
        Task HandleAsync(TCommand handled);
    }
}
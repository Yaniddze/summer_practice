using System.Threading.Tasks;

namespace TestApi.CQRS.Commands.Abstractions
{
    public interface ICommandHandler<TCommand> 
        where TCommand: ICommand
    {
        Task HandleAsync(TCommand handled);
    }
}
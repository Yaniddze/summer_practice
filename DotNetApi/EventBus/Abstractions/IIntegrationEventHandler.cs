using System.Threading.Tasks;

namespace EventBus.Abstractions
{
    public interface IIntegrationEventHandler<TE>
        where TE: IIntegrationEvent
    {
        Task HandleAsync(TE @event);
    }
}
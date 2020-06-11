using System.Threading.Tasks;

namespace MessageService.EventBus.Abstractions
{
    public interface IIntegrationEventHandler<TE>
        where TE: IIntegrationEvent
    {
        Task HandleAsync(TE @event);
    }
}
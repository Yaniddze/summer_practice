using System.Threading.Tasks;

namespace TestApi.EventBus.Abstractions
{
    public interface IIntegrationEventHandler<TE>
        where TE: IIntegrationEvent
    {
        Task HandleAsync(TE @event);
    }
}
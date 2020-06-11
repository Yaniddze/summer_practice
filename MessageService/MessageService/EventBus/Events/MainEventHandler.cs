using System;
using System.Threading.Tasks;
using MessageService.EventBus.Abstractions;

namespace MessageService.EventBus.Events
{
    public class MainEventHandler: IIntegrationEventHandler<MainIntegrationEvent>
    {
        public Task HandleAsync(MainIntegrationEvent @event)
        {
            Console.WriteLine(@event.UserId.ToString());
            return Task.CompletedTask;
        }
    }
}
using System;
using System.Threading.Tasks;
using EventBus.Abstractions;
using EventBus.Events;

namespace MessageService
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
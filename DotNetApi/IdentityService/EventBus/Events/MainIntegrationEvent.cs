using System;
using TestApi.EventBus.Abstractions;

namespace TestApi.EventBus.Events
{
    public class MainIntegrationEvent: IIntegrationEvent
    {
        public Guid UserId { get; set; }
    }
}
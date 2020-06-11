using System;
using MessageService.EventBus.Abstractions;

namespace MessageService.EventBus.Events
{
    public class MainIntegrationEvent: IIntegrationEvent
    {
        public Guid UserId { get; set; }
    }
}
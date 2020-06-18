using System;
using EventBus.Abstractions;

namespace EventBus.Events
{
    public class MainIntegrationEvent: IIntegrationEvent
    {
        public Guid UserId { get; set; }
    }
}
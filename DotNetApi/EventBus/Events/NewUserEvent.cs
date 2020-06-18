using System;
using EventBus.Abstractions;

namespace EventBus.Events
{
    public class NewUserEvent: IIntegrationEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public Guid ActivationUrl { get; set; }
    }
}
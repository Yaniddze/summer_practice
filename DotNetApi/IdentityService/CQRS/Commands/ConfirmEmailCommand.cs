using System;
using TestApi.CQRS.Commands.Abstractions;

namespace TestApi.CQRS.Commands
{
    public class ConfirmEmailCommand: ICommand
    {
        public Guid Activation { get; set; }
    }
}
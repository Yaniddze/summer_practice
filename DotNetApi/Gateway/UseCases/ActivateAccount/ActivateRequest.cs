using System;
using MediatR;

namespace Gateway.UseCases.ActivateAccount
{
    public class ActivateRequest: IRequest<ActivateAnswer>
    {
        public Guid Url { get; set; }
    }
}
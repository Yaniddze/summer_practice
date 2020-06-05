using System;
using MediatR;

namespace AspGateway.UseCases.ActivateAccount
{
    public class ActivateRequest: IRequest<ActivateAnswer>
    {
        public Guid Url { get; set; }
    }
}
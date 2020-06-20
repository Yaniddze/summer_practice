using System;
using MediatR;

namespace TestApi.UseCases.GenerateToken
{
    public class GenerateTokenRequest: IRequest<GenerateTokenAnswer>
    {
        public string Platform { get; set; }
        public Guid UserId { get; set; }
    }
}
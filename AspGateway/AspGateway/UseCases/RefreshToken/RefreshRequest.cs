using MediatR;

namespace AspGateway.UseCases.RefreshToken
{
    public class RefreshRequest: IRequest<RefreshAnswer>
    {
        public string Token { get; set; }
    }
}
using MediatR;

namespace Gateway.UseCases.RefreshToken
{
    public class RefreshRequest: IRequest<RefreshAnswer>
    {
        public string Token { get; set; }
        public string Platform { get; set; }
    }
}
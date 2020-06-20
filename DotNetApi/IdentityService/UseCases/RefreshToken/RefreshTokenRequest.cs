using MediatR;

namespace TestApi.UseCases.RefreshToken
{
    public class RefreshTokenRequest: IRequest<RefreshTokenAnswer>
    {
        public string Token { get; set; }
        public string Platform { get; set; }
    }
}
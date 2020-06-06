using MediatR;

namespace AspGateway.UseCases.Login
{
    public class LoginRequest: IRequest<LoginAnswer>
    {
        public string EmailOrLogin { get; set; }
        public string Password { get; set; }
    }
}
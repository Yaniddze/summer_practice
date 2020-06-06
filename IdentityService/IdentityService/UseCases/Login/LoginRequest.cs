using MediatR;

namespace TestApi.UseCases.Login
{
    public class LoginRequest : IRequest<LoginAnswer>
    {
        public string EmailOrLogin { get; set; }
        public string Password { get; set; }
    }
}
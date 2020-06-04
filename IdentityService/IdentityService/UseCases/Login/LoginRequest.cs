using MediatR;

namespace TestApi.UseCases.Login
{
    public class LoginRequest : IRequest<LoginAnswer>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
using MediatR;

namespace TestApi.UseCases.Registration
{
    public class RegistrationRequest: IRequest<RegistrationAnswer>
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Platform { get; set; }
    }
}
using MediatR;

namespace AspGateway.UseCases.Registration
{
    public class RegistrationRequest: IRequest<RegistrationAnswer>
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
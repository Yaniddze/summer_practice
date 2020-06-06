using FluentValidation;

namespace TestApi.UseCases.Login
{
    public class LoginRequestValidation: AbstractValidator<LoginRequest>
    {
        public LoginRequestValidation()
        {
            RuleFor(x => x.EmailOrLogin).NotNull();
            RuleFor(x => x.Password).NotNull();
        }
    }
}
using FluentValidation;
using TestApi.Options;

namespace TestApi.UseCases.Login
{
    public class LoginRequestValidation: AbstractValidator<LoginRequest>
    {
        public LoginRequestValidation(ValidPlatforms validPlatforms)
        {
            RuleFor(x => x.EmailOrLogin).NotNull();
            RuleFor(x => x.Password).NotNull();
            RuleFor(x => x.Platform)
                .NotNull()
                .Must(validPlatforms.Platforms.Contains)
                    .WithMessage("Platform must br one of " + string.Join(',', validPlatforms.Platforms));
        }
    }
}
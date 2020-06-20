using FluentValidation;
using TestApi.Options;

namespace TestApi.UseCases.RefreshToken
{
    public class RefreshRequestValidator: AbstractValidator<RefreshTokenRequest>
    {
        public RefreshRequestValidator(ValidPlatforms validPlatforms)
        {
            RuleFor(x => x.Token).NotNull();
            RuleFor(x => x.Platform)
                .NotNull()
                .Must(validPlatforms.Platforms.Contains)
                .WithMessage("Platform must br one of " + string.Join(',', validPlatforms.Platforms));
        }
    }
}
using FluentValidation;

namespace TestApi.UseCases.RefreshToken
{
    public class RefreshRequestValidator: AbstractValidator<RefreshTokenRequest>
    {
        public RefreshRequestValidator()
        {
            RuleFor(x => x.Token).NotNull();
        }
    }
}
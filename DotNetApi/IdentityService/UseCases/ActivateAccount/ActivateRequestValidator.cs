using FluentValidation;

namespace TestApi.UseCases.ActivateAccount
{
    public class ActivateRequestValidator: AbstractValidator<ActivateRequest>
    {
        public ActivateRequestValidator()
        {
            RuleFor(x => x.Url).NotNull();
        }
    }
}
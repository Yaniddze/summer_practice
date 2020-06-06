using System.Linq;
using FluentValidation;
using TestApi.Options;

namespace TestApi.UseCases.Registration
{
    public class RegistrationRequestValidator: AbstractValidator<RegistrationRequest>
    {
        public RegistrationRequestValidator(ValidEmails validEmails)
        {
            RuleFor(x => x.Email)
                .NotNull()
                .MaximumLength(50)
                .Must(x => validEmails.Emails.FirstOrDefault(x.EndsWith) != null)
                .WithMessage($"Email is not valid. Valid emails: {string.Join(',', validEmails.Emails)}");

            RuleFor(x => x.Login)
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(20);

            RuleFor(x => x.Password)
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(40);
        }
    }
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using TestApi.CQRS.Commands;
using TestApi.CQRS.Commands.Abstractions;

namespace TestApi.UseCases.ActivateAccount
{
    public class ActivateAccountUseCase : IRequestHandler<ActivateRequest, ActivateAnswer>
    {
        private readonly ICommandHandler<ConfirmEmailCommand> _confirmHandler;
        private readonly IValidator<ActivateRequest> _requestValidator;

        public ActivateAccountUseCase(
            IValidator<ActivateRequest> requestValidator,
            ICommandHandler<ConfirmEmailCommand> confirmHandler
        )
        {
            _requestValidator = requestValidator;
            _confirmHandler = confirmHandler;
        }

        public async Task<ActivateAnswer> Handle(ActivateRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _requestValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new ActivateAnswer
                {
                    Success = false,
                    Errors = validationResult.Errors
                        .Select(x => $"{x.PropertyName}: {x.ErrorMessage}")
                        .ToList()
                };
            }

            await _confirmHandler.HandleAsync(new ConfirmEmailCommand{ Activation = request.Url });

            return new ActivateAnswer
            {
                Success = true,
            };
        }
    }
}
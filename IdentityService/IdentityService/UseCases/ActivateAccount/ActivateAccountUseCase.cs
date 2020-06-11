using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using TestApi.CQRS.Commands;
using TestApi.CQRS.Queries;
using TestApi.DataBase.CQRS.Users.Commands.Update.ConfirmEmail;
using TestApi.Entities.User;

namespace TestApi.UseCases.ActivateAccount
{
    public class ActivateAccountUseCase : IRequestHandler<ActivateRequest, ActivateAnswer>
    {
        private readonly ICommandHandler<ConfirmEmailCommand> _confirmHandler;
        private readonly IFindQuery<User> _findQuery;
        private readonly IValidator<ActivateRequest> _requestValidator;

        public ActivateAccountUseCase(
            IValidator<ActivateRequest> requestValidator,
            ICommandHandler<ConfirmEmailCommand> confirmHandler,
            IFindQuery<User> findQuery
        )
        {
            _requestValidator = requestValidator;
            _confirmHandler = confirmHandler;
            _findQuery = findQuery;
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

            var founded = await _findQuery.FindOneAsync(user => user.UserEmail.ActivationUrl == request.Url);

            if (founded == null)
            {
                return new ActivateAnswer
                {
                    Success = false,
                    Errors = new List<string> {"Url is not valid"},
                };
            }

            if (founded.UserEmail.IsEmailConfirmed)
            {
                return new ActivateAnswer
                {
                    Success = false,
                    Errors = new List<string> {"Email already confirmed"},
                };
            }

            await _confirmHandler.HandleAsync(new ConfirmEmailCommand{ UserId = founded.Id });

            return new ActivateAnswer
            {
                Success = true,
            };
        }
    }
}
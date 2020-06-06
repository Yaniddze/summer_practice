using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using TestApi.Entities;
using TestApi.Repositories;

namespace TestApi.UseCases.ActivateAccount
{
    public class ActivateAccountUseCase : IRequestHandler<ActivateRequest, ActivateAnswer>
    {
        private readonly IRepository<User> _repository;
        private readonly IValidator<ActivateRequest> _requestValidator;

        public ActivateAccountUseCase(IRepository<User> repository, IValidator<ActivateRequest> requestValidator)
        {
            _repository = repository;
            _requestValidator = requestValidator;
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

            var founded = await _repository.FindOneWithPatternAsync(user => user.ActivationUrl == request.Url);

            if (founded == null)
            {
                return new ActivateAnswer
                {
                    Success = false,
                    Errors = new List<string>{ "Url is not valid" },
                };
            }

            if (founded.IsEmailConfirmed)
            {
                return new ActivateAnswer
                {
                    Success = false,
                    Errors = new List<string>{ "Email already confirmed" },
                };
            }

            founded.IsEmailConfirmed = true;

            await _repository.UpdateAsync(founded);

            return new ActivateAnswer
            {
                Success = true,
            };
        }
    }
}
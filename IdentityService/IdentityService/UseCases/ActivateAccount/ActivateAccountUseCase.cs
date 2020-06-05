using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestApi.Entities;
using TestApi.Repositories;

namespace TestApi.UseCases.ActivateAccount
{
    public class ActivateAccountUseCase : IRequestHandler<ActivateRequest, ActivateAnswer>
    {
        private readonly IRepository<User> _repository;

        public ActivateAccountUseCase(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<ActivateAnswer> Handle(ActivateRequest request, CancellationToken cancellationToken)
        {
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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using TestApi.Entities;
using TestApi.Repositories;

namespace TestApi.UseCases.Login
{
    public class LoginUseCase : IRequestHandler<LoginRequest, LoginAnswer>
    {
        private readonly IRepository<User> _repository;
        private readonly IValidator<LoginRequest> _loginValidator;

        public LoginUseCase(IRepository<User> repository, IValidator<LoginRequest> loginValidator)
        {
            _repository = repository;
            _loginValidator = loginValidator;
        }

        public async Task<LoginAnswer> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _loginValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new LoginAnswer
                {
                    Success = false,
                    Errors = validationResult.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}").ToList()
                };
            }

            var foundedUser = await _repository.FindOneWithPatternAsync(user =>
                (user.Email == request.EmailOrLogin || user.Login == request.EmailOrLogin) 
                && 
                user.Password == request.Password
            );

            if (foundedUser == null)
            {
                return new LoginAnswer
                {
                    Success = false,
                    Errors = new List<string> {"User not found"}
                };
            }

            if (!foundedUser.IsEmailConfirmed)
            {
                return new LoginAnswer
                {
                    Success = false,
                    Errors = new List<string> {"Email is not confirmed"}
                };
            }

            return new LoginAnswer
            {
                Success = true,
                UserId = foundedUser.Id,
                Email = foundedUser.Email,
            };
        }
    }
}
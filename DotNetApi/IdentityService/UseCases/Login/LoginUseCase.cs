using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using TestApi.CQRS.Queries;
using TestApi.CQRS.Queries.Abstractions;
using TestApi.Entities.Users;

namespace TestApi.UseCases.Login
{
    public class LoginUseCase : IRequestHandler<LoginRequest, LoginAnswer>
    {
        private readonly IValidator<LoginRequest> _loginValidator;
        private readonly IQueryHandler<AuthQuery, User> _finder;

        public LoginUseCase(IValidator<LoginRequest> loginValidator, IQueryHandler<AuthQuery, User> finder)
        {
            _loginValidator = loginValidator;
            _finder = finder;
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

            var foundedUser = await _finder.HandleAsync(new AuthQuery
            {
                Login = request.EmailOrLogin,
                Password = request.Password,
            });

            if (foundedUser is null)
            {
                return new LoginAnswer
                {
                    Success = false,
                    Errors = new List<string> {"User not found"}
                };
            }

            if (!foundedUser.UserEmail.IsEmailConfirmed)
            {
                return new LoginAnswer
                {
                    Success = false,
                    Errors = new List<string> { "Email is not confirmed" }
                };
            }
            
            return new LoginAnswer
            {
                Success = true,
                UserId = foundedUser.Id,
            };
        }
    }
}
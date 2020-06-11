using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using TestApi.CQRS.Queries;
using TestApi.Entities.User;
using TestApi.EventBus.Abstractions;
using TestApi.EventBus.Events;

namespace TestApi.UseCases.Login
{
    public class LoginUseCase : IRequestHandler<LoginRequest, LoginAnswer>
    {
        private readonly IValidator<LoginRequest> _loginValidator;
        private readonly IFindQuery<User> _finder;
        private readonly IEventBus _bus;

        public LoginUseCase(IValidator<LoginRequest> loginValidator, IFindQuery<User> finder, IEventBus bus)
        {
            _loginValidator = loginValidator;
            _finder = finder;
            _bus = bus;
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

            var foundedUser = await _finder.FindOneAsync(user =>
                (user.UserEmail.Email == request.EmailOrLogin || user.Login == request.EmailOrLogin) 
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

            if (!foundedUser.UserEmail.IsEmailConfirmed)
            {
                return new LoginAnswer
                {
                    Success = false,
                    Errors = new List<string> {"Email is not confirmed"}
                };
            }

            _bus.Publish(new MainIntegrationEvent{ UserId = foundedUser.Id}, nameof(MainIntegrationEvent));
            
            return new LoginAnswer
            {
                Success = true,
                UserId = foundedUser.Id,
                Email = foundedUser.UserEmail.Email,
            };
        }
    }
}
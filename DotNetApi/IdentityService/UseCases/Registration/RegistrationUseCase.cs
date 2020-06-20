using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventBus.Abstractions;
using EventBus.Events;
using FluentValidation;
using MediatR;
using TestApi.CQRS.Commands;
using TestApi.CQRS.Commands.Abstractions;
using TestApi.CQRS.Queries;
using TestApi.CQRS.Queries.Abstractions;
using TestApi.Entities.Users;

namespace TestApi.UseCases.Registration
{
    public class RegistrationUseCase : IRequestHandler<RegistrationRequest, RegistrationAnswer>
    {
        private readonly IValidator<RegistrationRequest> _requestValidator;
        private readonly ICommandHandler<AddUserCommand> _userAdder;
        private readonly IQueryHandler<UserByLoginQuery, User> _loginFinder;
        private readonly IQueryHandler<UserByEmailQuery, User> _emailFinder;
        private readonly IEventBus _bus;

        public RegistrationUseCase(
            IValidator<RegistrationRequest> requestValidator,
            ICommandHandler<AddUserCommand> userAdder, IEventBus bus, 
            IQueryHandler<UserByLoginQuery, User> loginFinder, 
            IQueryHandler<UserByEmailQuery, User> emailFinder)
        {
            _requestValidator = requestValidator;
            _userAdder = userAdder;
            _bus = bus;
            _loginFinder = loginFinder;
            _emailFinder = emailFinder;
        }

        public async Task<RegistrationAnswer> Handle(RegistrationRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _requestValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new RegistrationAnswer
                {
                    Success = false,
                    Errors = validationResult.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}").ToList(),
                };
            }

            var foundedLogin = await _loginFinder.HandleAsync(new UserByLoginQuery
            {
                Login = request.Login,
            });
            if (!(foundedLogin is null))
            {
                return new RegistrationAnswer
                {
                    Success = false,
                    Errors = new List<string> {"User with this login is already exist"}
                };
            }

            var foundedEmail = await _emailFinder.HandleAsync(new UserByEmailQuery
            {
                Email = request.Email,
            });
            if (!(foundedEmail is null))
            {
                return new RegistrationAnswer
                {
                    Success = false,
                    Errors = new List<string> {"User with this email is already exist"}
                };
            }

            var tempUser = new User
            {
                Id = Guid.NewGuid(),
                UserEmail = new UserEmail()
                {
                    IsEmailConfirmed = false,
                    ActivationUrl = Guid.NewGuid(),
                    Email = request.Email,
                },
                Login = request.Login,
                Password = request.Password,
            };

            await _userAdder.HandleAsync(new AddUserCommand
            {
                UserToAdd = tempUser,
            });

            _bus.Publish(new NewUserEvent
            {
                Email = request.Email,
                UserId = tempUser.Id,
                ActivationUrl = tempUser.UserEmail.ActivationUrl,
            }, nameof(NewUserEvent));

            return new RegistrationAnswer
            {
                Success = true,
                UserId = tempUser.Id,
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using TestApi.Entities.User;
using TestApi.Repositories;

namespace TestApi.UseCases.Registration
{
    public class RegistrationUseCase: IRequestHandler<RegistrationRequest, RegistrationAnswer>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IValidator<RegistrationRequest> _requestValidator;
        
        public RegistrationUseCase(IRepository<User> userRepository, IValidator<RegistrationRequest> requestValidator)
        {
            _userRepository = userRepository;
            _requestValidator = requestValidator;
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

            var foundedLogin = await _userRepository.FindOneWithPatternAsync(user => user.Login == request.Login);
            if (!(foundedLogin is null))
            {
                return new RegistrationAnswer
                {
                    Success = false,
                    Errors = new List<string>{ "User with this login is already exist" }
                };
            }

            var foundedEmail = await _userRepository.FindOneWithPatternAsync(user => user.UserEmail.Email == request.Email);
            if (!(foundedEmail is null))
            {
                return new RegistrationAnswer
                {
                    Success = false,
                    Errors = new List<string>{ "User with this email is already exist" }
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
                Name = "",
                Password = request.Password,
            };
            
            await _userRepository.InsertAsync(tempUser);

            return new RegistrationAnswer
            {
                Success = true,
                Email = request.Email,
                UserId = tempUser.Id,
                ActivationUrl = tempUser.UserEmail.ActivationUrl,
            };
        }
    }
}

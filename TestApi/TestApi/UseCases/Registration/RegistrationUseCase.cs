using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestApi.Entities;
using TestApi.Repositories;

namespace TestApi.UseCases.Registration
{
    public class RegistrationUseCase: IRequestHandler<RegistrationRequest, RegistrationAnswer>
    {
        private readonly IRepository<User> _userRepository;

        public RegistrationUseCase(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RegistrationAnswer> Handle(RegistrationRequest request, CancellationToken cancellationToken)
        {
            var foundedLogin = await _userRepository.FindOneWithPatternAsync(user => user.Login == request.Login);
            if (!(foundedLogin is null))
            {
                return new RegistrationAnswer
                {
                    Success = false,
                    Errors = new List<string>{ "User with this login is already exist" }
                };
            }

            var foundedEmail = await _userRepository.FindOneWithPatternAsync(user => user.Email == request.Email);
            if (!(foundedEmail is null))
            {
                return new RegistrationAnswer
                {
                    Success = false,
                    Errors = new List<string>{ "User with this email is already exist" }
                };
            }

            await _userRepository.InsertAsync(new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                IsEmailConfirmed = false,
                Login = request.Login,
                Name = "",
                Password = request.Password,
            });

            return new RegistrationAnswer
            {
                Success = true,
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestApi.Entities;
using TestApi.Options;
using TestApi.Repositories;

namespace TestApi.UseCases.Registration
{
    public class RegistrationUseCase: IRequestHandler<RegistrationRequest, RegistrationAnswer>
    {
        private readonly IRepository<User> _userRepository;
        private readonly ValidEmails _emails;
        
        public RegistrationUseCase(IRepository<User> userRepository, ValidEmails emails)
        {
            _userRepository = userRepository;
            _emails = emails;
        }

        public async Task<RegistrationAnswer> Handle(RegistrationRequest request, CancellationToken cancellationToken)
        {
            if (_emails.Emails.FirstOrDefault(x => request.Email.EndsWith(x)) == null)
            {
                return new RegistrationAnswer
                {
                    Success = false,
                    Errors = new List<string>{ $"Email is not valid. Valid emails: {string.Join(',', _emails.Emails)}" }
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

            var foundedEmail = await _userRepository.FindOneWithPatternAsync(user => user.Email == request.Email);
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
                Email = request.Email,
                IsEmailConfirmed = false,
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
            };
        }
    }
}
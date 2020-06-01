using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.UseCases.Registration
{
    public class RegistrationUseCase
    {
        private readonly IRepository<User> _userRepository;

        public RegistrationUseCase(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RegistrationAnswer> RegisterAsync(string email, string login, string password)
        {
            
            var foundedLogin = await _userRepository.FindOneWithPattern(user => user.Login == login);
            if (!(foundedLogin is null))
            {
                return new RegistrationAnswer
                {
                    Success = false,
                    Errors = new List<string>{ "User with this login is already exist" }
                };
            }

            var foundedEmail = await _userRepository.FindOneWithPattern(user => user.Email == email);
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
                Email = email,
                IsEmailConfirmed = false,
                Login = login,
                Name = "",
                Password = password,
            });

            return new RegistrationAnswer
            {
                Success = true,
            };
        }
    }
}
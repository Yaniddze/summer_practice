using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestApi.Entities;
using TestApi.Repositories;

namespace TestApi.UseCases.Login
{
    public class LoginUseCase : IRequestHandler<LoginRequest, LoginAnswer>
    {
        private readonly IRepository<User> _repository;

        public LoginUseCase(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<LoginAnswer> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var foundedUser = await _repository.FindOneWithPatternAsync(user =>
                (user.Email == request.Email || user.Login == request.Email) 
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
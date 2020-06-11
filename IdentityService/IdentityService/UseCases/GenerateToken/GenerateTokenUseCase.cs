using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using TestApi.CQRS.Commands;
using TestApi.CQRS.Queries;
using TestApi.DataBase.CQRS.Users.Commands.Update.WriteToken;
using TestApi.Entities.User;
using TestApi.Options;

namespace TestApi.UseCases.GenerateToken
{
    public class GenerateTokenUseCase : IRequestHandler<GenerateTokenRequest, GenerateTokenAnswer>
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IGetByIdQuery<User> _getByIdQuery;
        private readonly ICommandHandler<WriteTokenCommand> _commandHandler;

        public GenerateTokenUseCase(
            JwtOptions jwtOptions,
            IGetByIdQuery<User> getByIdQuery,
            ICommandHandler<WriteTokenCommand> commandHandler
        )
        {
            _jwtOptions = jwtOptions;
            _getByIdQuery = getByIdQuery;
            _commandHandler = commandHandler;
        }

        public async Task<GenerateTokenAnswer> Handle(GenerateTokenRequest request, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, request.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, request.Email),
                    new Claim("id", request.UserId.ToString()),
                }),
//            LifeTime: 5 Minutes by default + 0.01 minutes
                Expires = DateTime.UtcNow.AddMinutes(0.01),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtOptions.SecretInBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var foundedUser = await _getByIdQuery.InvokeAsync(request.UserId);
            if (foundedUser == null)
            {
                return new GenerateTokenAnswer();
            }

            var tokenToUpdate = new UserToken
            {
                Token = tokenHandler.WriteToken(token),
                JwtId = token.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
            };

            await _commandHandler.HandleAsync(new WriteTokenCommand
            {
                UserId = foundedUser.Id,
                NewToken = tokenToUpdate
            });

            return new GenerateTokenAnswer
            {
                Token = tokenToUpdate.Token,
            };
        }
    }
}
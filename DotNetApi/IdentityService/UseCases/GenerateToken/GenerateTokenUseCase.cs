using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using TestApi.CQRS.Commands;
using TestApi.CQRS.Commands.Abstractions;
using TestApi.Options;

namespace TestApi.UseCases.GenerateToken
{
    public class GenerateTokenUseCase : IRequestHandler<GenerateTokenRequest, GenerateTokenAnswer>
    {
        private readonly JwtOptions _jwtOptions;
        private readonly ICommandHandler<WriteTokenCommand> _commandHandler;

        public GenerateTokenUseCase(
            JwtOptions jwtOptions,
            ICommandHandler<WriteTokenCommand> commandHandler
        )
        {
            _jwtOptions = jwtOptions;
            _commandHandler = commandHandler;
        }

        public async Task<GenerateTokenAnswer> Handle(GenerateTokenRequest request, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("id", request.UserId.ToString()),
                }),
//            LifeTime: 5 Minutes by default + 0.01 minutes
                Expires = DateTime.UtcNow.AddMinutes(0.01),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtOptions.SecretInBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            var tempCommand = new WriteTokenCommand
            {
                UserId = request.UserId,
                ExpiryDate = DateTime.UtcNow.AddYears(2),
                JwtId = token.Id,
                Platform = request.Platform,
                Token = tokenHandler.WriteToken(token),
            };
            
            await _commandHandler.HandleAsync(tempCommand);

            return new GenerateTokenAnswer
            {
                Token = tempCommand.Token,
            };
        }
    }
}
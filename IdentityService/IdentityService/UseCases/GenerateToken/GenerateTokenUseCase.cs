using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using TestApi.Entities;
using TestApi.Entities.User;
using TestApi.Options;
using TestApi.Repositories;

namespace TestApi.UseCases.GenerateToken
{
    public class GenerateTokenUseCase: IRequestHandler<GenerateTokenRequest, GenerateTokenAnswer>
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IRepository<User> _userRepository;

        public GenerateTokenUseCase(JwtOptions jwtOptions, IRepository<User> userRepository)
        {
            _jwtOptions = jwtOptions;
            _userRepository = userRepository;
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
            
            var foundedUser = await _userRepository.GetByIdAsync(request.UserId);
            if (foundedUser == null)
            {
                return new GenerateTokenAnswer();
            }

            foundedUser.UserToken.Token = tokenHandler.WriteToken(token);
            foundedUser.UserToken.JwtId = token.Id;
            foundedUser.UserToken.CreationDate = DateTime.UtcNow;
            foundedUser.UserToken.ExpiryDate = DateTime.UtcNow.AddMonths(6);

            await _userRepository.UpdateAsync(foundedUser);
            
            return new GenerateTokenAnswer
            {
                Token = foundedUser.UserToken.Token,
            }; 
        }
    }
}
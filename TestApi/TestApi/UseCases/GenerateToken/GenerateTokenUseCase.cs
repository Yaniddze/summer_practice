using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using TestApi.Entities;
using TestApi.Options;
using TestApi.Repositories;

namespace TestApi.UseCases.GenerateToken
{
    public class GenerateTokenUseCase: IRequestHandler<GenerateTokenRequest, GenerateTokenAnswer>
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IRepository<RefreshToken> _tokenRepository;

        public GenerateTokenUseCase(JwtOptions jwtOptions, IRepository<RefreshToken> tokenRepository)
        {
            _jwtOptions = jwtOptions;
            _tokenRepository = tokenRepository;
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
//              LifeTime: 5 Minutes by default + 0.01 minutes
                Expires = DateTime.UtcNow.AddMinutes(0.01),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtOptions.SecretInBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                JwtId = token.Id,
                UserId = request.UserId,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
            };

            await _tokenRepository.InsertAsync(refreshToken);
            
            return new GenerateTokenAnswer
            {
                Token = tokenHandler.WriteToken(token),
                RefreshedToken = refreshToken.Id.ToString()
            }; 
        }
    }
}
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using TestApi.Entities;
using TestApi.Repositories;

namespace TestApi.UseCases.RefreshToken
{
    public class RefreshTokenUseCase: IRequestHandler<RefreshTokenRequest, RefreshTokenAnswer>
    {
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IRepository<User> _userRepository;


        public RefreshTokenUseCase(TokenValidationParameters tokenValidationParameters, IRepository<User> userRepository)
        {
            _tokenValidationParameters = tokenValidationParameters;
            _userRepository = userRepository;
        }

        public async Task<RefreshTokenAnswer> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var validatedToken = GetPrincipalFromToken(request.Token);

            if (validatedToken == null)
            {
                return new RefreshTokenAnswer
                {
                    Success = false,
                    Errors = new List<string>{ "Invalid token" }
                };
            }

            var expiryDateUnix =
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateUtc > DateTime.UtcNow)
            {
                return new RefreshTokenAnswer
                {
                    Success = false,
                    Errors = new List<string>{ "This token hasn't expired yet" }
                };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedUser = await _userRepository.FindOneWithPatternAsync(user => user.UserToken.Token == request.Token);
            if (storedUser == null)
            {
                return new RefreshTokenAnswer
                {
                    Success = false,
                    Errors = new List<string>{ "User with this token doesn't exists" }
                };
            }

            if (DateTime.UtcNow > storedUser.UserToken.ExpiryDate)
            {
                return new RefreshTokenAnswer
                {
                    Success = false,
                    Errors = new List<string>{ "This refresh token has expired" }
                };
            }

            if (storedUser.UserToken.JwtId != jti)
            {
                return new RefreshTokenAnswer
                {
                    Success = false,
                    Errors = new List<string>{ "This refresh token does not match this JWT" }
                };
            }
            
            return new RefreshTokenAnswer
            {
                Success = true,
                Email = storedUser.Email,
                UserId = storedUser.Id,
            };
        }
        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                return !IsJwtValidSecurityAlgorithm(validatedToken) ? null : principal;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static bool IsJwtValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
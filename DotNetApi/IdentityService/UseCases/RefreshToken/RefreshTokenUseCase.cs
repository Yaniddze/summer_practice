using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using TestApi.CQRS.Queries;
using TestApi.CQRS.Queries.Abstractions;
using TestApi.Entities.Tokens;

namespace TestApi.UseCases.RefreshToken
{
    public class RefreshTokenUseCase : IRequestHandler<RefreshTokenRequest, RefreshTokenAnswer>
    {
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IValidator<RefreshTokenRequest> _requestValidator;
        private readonly IQueryHandler<TokenQuery, Token> _finder;

        public RefreshTokenUseCase(
            TokenValidationParameters tokenValidationParameters,
            IValidator<RefreshTokenRequest> requestValidator, 
            IQueryHandler<TokenQuery, Token> finder)
        {
            _tokenValidationParameters = tokenValidationParameters;
            _requestValidator = requestValidator;
            _finder = finder;
        }

        public async Task<RefreshTokenAnswer> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _requestValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new RefreshTokenAnswer
                {
                    Success = false,
                    Errors = validationResult.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}").ToList()
                };
            }

            var validatedToken = GetPrincipalFromToken(request.Token);

            if (validatedToken is null)
            {
                return new RefreshTokenAnswer
                {
                    Success = false,
                    Errors = new List<string> {"Invalid token"}
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
                    Errors = new List<string> {"This token hasn't expired yet"}
                };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedToken = await _finder.HandleAsync(new TokenQuery
            {
                Token = request.Token,
            });
            if (storedToken is null)
            {
                return new RefreshTokenAnswer
                {
                    Success = false,
                    Errors = new List<string> { "This token doesn't exists" }
                };
            }

            if (DateTime.UtcNow > storedToken.ExpiryDate)
            {
                return new RefreshTokenAnswer
                {
                    Success = false,
                    Errors = new List<string> {"This refresh token has expired"}
                };
            }

            if (storedToken.JwtId != jti)
            {
                return new RefreshTokenAnswer
                {
                    Success = false,
                    Errors = new List<string> {"This refresh token does not match this JWT"}
                };
            }

            return new RefreshTokenAnswer
            {
                Success = true,
                UserId = storedToken.UserId,
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
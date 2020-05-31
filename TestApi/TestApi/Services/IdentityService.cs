using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using TestApi.Controllers.Contract.Responses;
using TestApi.Options;

namespace TestApi.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private static List<User> users = new List<User>();
        private static List<RefreshToken> refreshTokens = new List<RefreshToken>();

        public IdentityService(JwtOptions jwtOptions, TokenValidationParameters tokenValidationParameters)
        {
            _jwtOptions = jwtOptions;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public Task<RegisterResponse> registerAsync(string requestEmail, string requestPassword)
        {
            var tempUser = new User
            {
                id = Guid.NewGuid(),
                email = requestEmail,
                password = requestPassword,
            };
            users.Add(tempUser);
            
            var generatedToken = GenerateToken(requestEmail, tempUser.id);

            return Task.FromResult(new RegisterResponse
            {
                isRegistered = true,
                token = generatedToken.token,
                refreshToken = generatedToken.refreshedToken
            });
        }

        public Task<LoginResponse> loginAsync(string requestEmail, string requestPassword)
        {
            var foundedUser = users.FirstOrDefault(x => x.email == requestEmail && x.password == requestPassword);

            if (foundedUser == null)
            {
                return Task.FromResult(new LoginResponse
                {
                    isSuccess = false,
                    errors = new List<string> { "user doesn't exists" }
                });
            }

            var generatedToken = GenerateToken(requestEmail, foundedUser.id);
            
            return Task.FromResult(new LoginResponse
            {
                isSuccess = true,
                token = generatedToken.token,
                refreshToken = generatedToken.refreshedToken,
            });
        }

        public async Task<RefreshTokenResponse> refreshTokenAsync(string requestToken, string requestRefreshToken)
        {
            var validatedToken = getPrincipalFromToken(requestToken);

            if (validatedToken == null)
            {
                return new RefreshTokenResponse
                {
                    errors = new List<string>{ "Invalid token" }
                };
            }

            var expiryDateUnix =
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateUtc > DateTime.UtcNow)
            {
                return new RefreshTokenResponse
                {
                    errors = new List<string>{ "This token hasn't expired yet" }
                };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = refreshTokens.SingleOrDefault(x => x.Token == requestRefreshToken);

            if (storedRefreshToken == null)
            {
                return new RefreshTokenResponse
                {
                    errors = new List<string>{ "This token doesn't exists" }
                };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new RefreshTokenResponse
                {
                    errors = new List<string>{ "This refresh token has expired" }
                };
            }

            if (storedRefreshToken.Invalidated)
            {
                return new RefreshTokenResponse
                {
                    errors = new List<string>{ "This token has been invalidated" }
                };
            }

            if (storedRefreshToken.Used)
            {
                return new RefreshTokenResponse
                {
                    errors = new List<string>{ "This refresh token has been used" }
                };
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new RefreshTokenResponse
                {
                    errors = new List<string>{ "This refresh token does not match this JWT" }
                };
            }

            storedRefreshToken.Used = true;
            // Save token in DB

            var user = users.First(x => x.id.ToString()== validatedToken.Claims.Single(xx => xx.Type == "id").Value);

            var generatedToken = GenerateToken(user.email, user.id);
            
            return new RefreshTokenResponse
            {
                token = generatedToken.token,
                refreshToken = generatedToken.refreshedToken,
            };
        }

        private ClaimsPrincipal getPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!isJwtValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private bool isJwtValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }

        class GeneratedToken
        {
            public string token { get; set; }
            public string refreshedToken { get; set; }
        }

        private GeneratedToken GenerateToken(string email, Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, email),
                    new Claim("id", userId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.TokenLifeTimeInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtOptions.SecretInBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                JwtId = token.Id,
                User = users.Find(x => x.id == userId),
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
            };
            
            refreshTokens.Add(refreshToken);
            
            return new GeneratedToken
            {
                token = tokenHandler.WriteToken(token),
                refreshedToken = refreshToken.Token
            }; 
        }
    }

    class User
    {
        public Guid id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }

    class RefreshToken
    {
        public string Token { get; set; }
        public string JwtId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }
        public bool Invalidated { get; set; }
        public User User { get; set; }
    }
}
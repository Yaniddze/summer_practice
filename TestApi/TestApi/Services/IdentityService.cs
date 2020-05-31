using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using TestApi.Controllers.Contract.Responses;
using TestApi.Options;

namespace TestApi.Services
{
    public class IdentityService: IIdentityService
    {
        private JwtOptions _jwtOptions;
        private static List<User> users = new List<User>();

        public IdentityService(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public Task<RegisterResponse> registerAsync(string requestEmail, string requestPassword)
        {
            users.Add(new User
            {
                email = requestEmail,
                password = requestPassword,
            });

            return Task.FromResult(new RegisterResponse
            {
                isRegistered = true,
                token = GenerateToken(requestEmail)
            });
        }

        private string GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new [] 
                {
                    new Claim(JwtRegisteredClaimNames.Sub, email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
                    new Claim(JwtRegisteredClaimNames.Email, email),
                    new Claim("id", Guid.NewGuid().ToString()), 
                }),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtOptions.SecretInBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    class User
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
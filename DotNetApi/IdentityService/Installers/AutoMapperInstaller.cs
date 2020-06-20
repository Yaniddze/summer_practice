using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApi.DataBase.Entities;
using TestApi.Entities.Platforms;
using TestApi.Entities.Tokens;
using TestApi.Entities.Users;

namespace TestApi.Installers
{
    public class AutoMapperInstaller : IInstaller
    {
        public void installServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(options =>
            {
                options.AddExpressionMapping();
                
                // Map user to userDB
                options.CreateMap<User, UserDB>()
                    .ForMember(x => x.activation_url,
                        map => map.MapFrom(
                            dest => dest.UserEmail.ActivationUrl
                        ))
                    .ForMember(x => x.email,
                        map => map.MapFrom(
                            dest => dest.UserEmail.Email
                        ))
                    .ForMember(x => x.is_email_confirmed,
                        map => map.MapFrom(
                            dest => dest.UserEmail.IsEmailConfirmed
                        ))
                    .ForMember(x => x.id,
                        map => map.MapFrom(
                            dest => dest.Id
                        ))
                    .ForMember(x => x.login,
                        map => map.MapFrom(
                            dest => dest.Login
                        ))
                    .ForMember(x => x.password,
                        map => map.MapFrom(
                            dest => dest.Password
                        ));
                
                // Map userDB to user
                options.CreateMap<UserDB, User>()
                    .ForMember(x => x.UserEmail,
                        map => map.MapFrom(
                            dest => new UserEmail
                            {
                                ActivationUrl = dest.activation_url,
                                Email = dest.email,
                                IsEmailConfirmed = dest.is_email_confirmed,
                            }
                        ))
                    .ForMember(x => x.Login,
                        map => map.MapFrom(
                            dest => dest.login
                        ))
                    .ForMember(x => x.Password,
                        map => map.MapFrom(
                            dest => dest.password
                        ))
                    .ForMember(x => x.Id,
                        map => map.MapFrom(
                            dest => dest.id
                        ));

                // Map token to tokenDB
                options.CreateMap<Token, TokenDB>()
                    .ForMember(x => x.id,
                        map => map.MapFrom(
                            dest => dest.Id
                        ))
                    .ForMember(x => x.token,
                        map => map.MapFrom(
                            dest => dest.TokenValue
                        ))
                    .ForMember(x => x.creation_date,
                        map => map.MapFrom(
                            dest => dest.CreationDate
                        ))
                    .ForMember(x => x.expiry_date,
                        map => map.MapFrom(
                            dest => dest.ExpiryDate
                        ))
                    .ForMember(x => x.jwt_id,
                        map => map.MapFrom(
                            dest => dest.JwtId
                        ))
                    .ForMember(x => x.user_id,
                        map => map.MapFrom(
                            dest => dest.UserId    
                        ));
                
                // Map tokenDB to token
                options.CreateMap<TokenDB, Token>()
                    .ForMember(x => x.Id,
                        map => map.MapFrom(
                            dest => dest.id
                        ))
                    .ForMember(x => x.TokenValue,
                        map => map.MapFrom(
                            dest => dest.token
                        ))
                    .ForMember(x => x.CreationDate,
                        map => map.MapFrom(
                            dest => dest.creation_date
                        ))
                    .ForMember(x => x.ExpiryDate,
                        map => map.MapFrom(
                            dest => dest.expiry_date
                        ))
                    .ForMember(x => x.JwtId,
                        map => map.MapFrom(
                            dest => dest.jwt_id
                        ))
                    .ForMember(x => x.UserId,
                        map => map.MapFrom(
                            dest => dest.user_id    
                        ));
                
                // Map platform to PlatformDB
                options.CreateMap<Platform, PlatformDB>()
                    .ForMember(x => x.id,
                        map => map.MapFrom(
                            dest => dest.Id
                        ))
                    .ForMember(x => x.title,
                        map => map.MapFrom(
                            dest => dest.Title
                        ));
                
                // Map platformDB to platform
                options.CreateMap<PlatformDB, Platform>()
                    .ForMember(x => x.Id,
                        map => map.MapFrom(
                            dest => dest.id
                        ))
                    .ForMember(x => x.Title,
                        map => map.MapFrom(
                            dest => dest.title
                        ));
            }, typeof(Startup));
        }
    }
}
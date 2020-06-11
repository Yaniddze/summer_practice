using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApi.DataBase.Entities;
using TestApi.Entities.User;

namespace TestApi.Installers
{
    public class AutoMapperInstaller : IInstaller
    {
        public void installServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(options =>
            {
                options.AddExpressionMapping();
                options.CreateMap<UserDB, User>()
                    .ForMember(
                        destination => destination.UserToken,
                        map => map.MapFrom(
                            source => new UserToken
                            {
                                Token = source.token,
                                CreationDate = source.creation_date,
                                ExpiryDate = source.expiry_date,
                                JwtId = source.jwtid,
                            }
                        ))
                    .ForMember(
                        destination => destination.UserEmail,
                        map => map.MapFrom(
                            source => new UserEmail
                            {
                                Email = source.email,
                                ActivationUrl = source.activation_url,
                                IsEmailConfirmed = source.isemailconfirmed,
                            }
                        ));
                options.CreateMap<User, UserDB>()
                    .ForMember(
                        x => x.creation_date,
                        map => map.MapFrom(source => source.UserToken.CreationDate)
                    )
                    .ForMember(
                        x => x.expiry_date,
                        map => map.MapFrom(source => source.UserToken.ExpiryDate)
                    )
                    .ForMember(
                        x => x.token,
                        map => map.MapFrom(source => source.UserToken.Token)
                    )
                    .ForMember(
                        x => x.jwtid,
                        map => map.MapFrom(source => source.UserToken.JwtId)
                    )
                    .ForMember(
                        x => x.activation_url,
                        map => map.MapFrom(source => source.UserEmail.ActivationUrl)
                    )
                    .ForMember(
                        x => x.email,
                        map => map.MapFrom(source => source.UserEmail.Email)
                    )
                    .ForMember(
                        x => x.isemailconfirmed,
                        map => map.MapFrom(source => source.UserEmail.IsEmailConfirmed)
                    );
            }, typeof(Startup));
        }
    }
}
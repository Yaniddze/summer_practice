using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApi.DataBase.Entities;
using TestApi.Entities;

namespace TestApi.Installers
{
    public class AutoMapperInstaller: IInstaller
    {
        public void installServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(options =>
            {
                options.CreateMap<UserDB, User>()
                    .ForMember(
                        destination => destination.UserToken, 
                        map => map.MapFrom(
                        source => new UserToken
                        {
                            Token = source.token,
                            CreationDate = source.creation_date,
                            ExpiryDate = source.expiry_date,
                            JwtId = source.JwtId,
                        }
                    ));
            },typeof(Startup));
        }
    }
}
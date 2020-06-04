using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApi.DataBase;
using TestApi.DataBase.Repositories;
using TestApi.Entities;
using TestApi.Repositories;

namespace TestApi.Installers
{
    public class DependenciesInstaller: IInstaller
    {
        public void installServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new Context());
//            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<User>, UsersRepository>();
        }
    }
}
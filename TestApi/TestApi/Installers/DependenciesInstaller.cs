using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApi.Repositories;
using TestApi.Services;

namespace TestApi.Installers
{
    public class DependenciesInstaller: IInstaller
    {
        public void installServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IRepository<TestApi.Entities.User>, UserRepository>();
            services.AddScoped<IRepository<TestApi.Entities.RefreshToken>, TokenRepository>();
        }
    }
}
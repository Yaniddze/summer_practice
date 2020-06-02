using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApi.Repositories;

namespace TestApi.Installers
{
    public class DependenciesInstaller: IInstaller
    {
        public void installServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepository<TestApi.Entities.User>, UserRepository>();
        }
    }
}
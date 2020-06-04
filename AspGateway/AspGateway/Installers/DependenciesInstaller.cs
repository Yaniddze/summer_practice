using System.Net.Http;
using AspGateway.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspGateway.Installers
{
    public class DependenciesInstaller: IInstaller
    {
        public void installServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new HttpClient());
            services.AddSingleton(new Urls
            {
                Identity = "http://identity-service:80"
            });
        }
    }
}

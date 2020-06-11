using System.Net.Http;
using Gateway.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Installers
{
    public class DependenciesInstaller: IInstaller
    {
        public void installServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new HttpClient());
            services.AddSingleton(new Urls
            {
                Identity = "http://identity-service:80",
                Streaming = "http://streaming-service:80",
            });
        }
    }
}

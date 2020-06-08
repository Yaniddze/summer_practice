using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StreamingApi.Installers
{
    public class MediatRInstaller: IInstaller
    {
        public void installServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(Startup));
        }
    }
}
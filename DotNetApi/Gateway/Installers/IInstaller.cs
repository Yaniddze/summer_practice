using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Installers
{
    public interface IInstaller
    {
        void installServices(IServiceCollection services, IConfiguration configuration);
    }
}
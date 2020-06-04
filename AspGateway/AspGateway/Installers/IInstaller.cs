using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspGateway.Installers
{
    public interface IInstaller
    {
        void installServices(IServiceCollection services, IConfiguration configuration);
    }
}
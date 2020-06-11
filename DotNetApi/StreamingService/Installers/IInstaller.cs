using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StreamingService.Installers
{
    public interface IInstaller
    {
        void installServices(IServiceCollection services, IConfiguration configuration);
    }
}
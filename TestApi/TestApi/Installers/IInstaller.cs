using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestApi.Installers
{
    public interface IInstaller
    {
        void installServices(IServiceCollection services, IConfiguration configuration);
    }
}
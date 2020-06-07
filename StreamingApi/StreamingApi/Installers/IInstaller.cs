using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StreamingApi.Installers
{
    public interface IInstaller
    {
        void installServices(IServiceCollection services, IConfiguration configuration);
    }
}
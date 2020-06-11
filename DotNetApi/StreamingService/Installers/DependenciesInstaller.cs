using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StreamingService.Database;
using StreamingService.Entities;
using StreamingService.Repositories;
using StreamingService.UseCases.AddSong;
using StreamingService.UseCases.GetSong;

namespace StreamingService.Installers
{
    public class DependenciesInstaller: IInstaller
    {
        public void installServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepository<Song>, SongsRepository>();
            services.AddScoped<IValidator<AddSongRequest>, AddSongRequestValidator>();
            services.AddScoped<IValidator<GetSongRequest>, GetSongRequestValidator>();
        }
    }
}
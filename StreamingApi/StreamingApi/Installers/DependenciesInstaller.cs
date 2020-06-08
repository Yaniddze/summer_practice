using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StreamingApi.Database;
using StreamingApi.Entities;
using StreamingApi.Repositories;
using StreamingApi.UseCases.AddSong;
using StreamingApi.UseCases.GetSong;

namespace StreamingApi.Installers
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
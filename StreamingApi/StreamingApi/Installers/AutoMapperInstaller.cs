using System;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StreamingApi.Database;
using StreamingApi.Entities;
using StreamingApi.UseCases.AddSong;

namespace StreamingApi.Installers
{
    public class AutoMapperInstaller : IInstaller
    {
        public void installServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(options =>
            {
                options.AddExpressionMapping();
                options.CreateMap<AddSongRequest, Song>()
                    .ForMember(des => des.Artist,
                        map => map.MapFrom(x => x.Artist))
                    .ForMember(des => des.Content,
                        map => map.MapFrom(x => Convert.FromBase64String(x.Content)))
                    .ForMember(des => des.Title,
                        map => map.MapFrom(x => x.Title))
                    .ForMember(des => des.Id,
                        map => map.Ignore());

                options.CreateMap<Song, SongDB>()
                    .ForMember(des => des.Artist,
                        map => map.MapFrom(x => x.Artist))
                    .ForMember(des => des.Content,
                        map => map.MapFrom(x => x.Content))
                    .ForMember(des => des.Id,
                        map => map.MapFrom(x => x.Id))
                    .ForMember(des => des.Title,
                        map => map.MapFrom(x => x.Title));

                options.CreateMap<SongDB, Song>()
                    .ForMember(des => des.Artist,
                        map => map.MapFrom(x => x.Artist))
                    .ForMember(des => des.Content,
                        map => map.MapFrom(x => x.Content))
                    .ForMember(des => des.Id,
                        map => map.MapFrom(x => x.Id))
                    .ForMember(des => des.Title,
                        map => map.MapFrom(x => x.Title));
            }, typeof(Startup));
        }
    }
}
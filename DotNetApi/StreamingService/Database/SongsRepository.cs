using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using StreamingService.Entities;
using StreamingService.Repositories;

namespace StreamingService.Database
{
    public class SongsRepository: IRepository<Song>
    {
        private readonly IMongoDatabase _mongo;
        private readonly IMapper _mapper;
        private const string Collection = "music";

        public SongsRepository(IMongoDatabase mongo, IMapper mapper)
        {
            _mongo = mongo;
            _mapper = mapper;
        }

        public async Task<Song> GetByTemplateAsync(Expression<Func<Song, bool>> pattern)
        {
            var collection = _mongo.GetCollection<SongDB>(Collection);
            var mappedPattern = _mapper.Map<Expression<Func<Song, bool>>, Expression<Func<SongDB, bool>>>(pattern);
            var result = await collection.Find(mappedPattern).FirstOrDefaultAsync();

            return result == null ? null : _mapper.Map<SongDB, Song>(result);
        }

        public async Task InsertAsync(Song entity)
        {
            var collection = _mongo.GetCollection<SongDB>(Collection);
            await collection.InsertOneAsync(_mapper.Map<Song, SongDB>(entity));
        }
    }
}
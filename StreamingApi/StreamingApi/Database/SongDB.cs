using System;
using MongoDB.Bson.Serialization.Attributes;

namespace StreamingApi.Database
{
    public class SongDB
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public byte[] Content { get; set; }
    }
}
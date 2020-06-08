using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace StreamingApi.Installers
{
    public class MongoInstaller: IInstaller
    {
        public void installServices(IServiceCollection services, IConfiguration configuration)
        {
            var connString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");

            var client = new MongoClient(connString);
            services.AddSingleton(client.GetDatabase(dbName));
        }
    }
}
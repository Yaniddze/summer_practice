using System;
using Microsoft.EntityFrameworkCore;
using TestApi.DataBase.Entities;

namespace TestApi.DataBase.Context
{
    public class EntityContext: DbContext
    {
        public DbSet<UserDB> users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? throw new NullReferenceException());
        }
    }
}

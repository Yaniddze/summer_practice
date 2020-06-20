using System.Data;
using Npgsql;

namespace TestApi.DataBase
{
    public class ContextProvider
    {
        private readonly string _connString;
        public ContextProvider(string connString)
        {
            _connString = connString;
        }
        public IDbConnection GetConnection()
        {
            return new NpgsqlConnection(_connString);
        }
    }
}
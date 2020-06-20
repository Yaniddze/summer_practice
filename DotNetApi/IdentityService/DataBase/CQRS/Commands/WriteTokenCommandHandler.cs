using System;
using System.Threading.Tasks;
using Dapper;
using TestApi.CQRS.Commands;
using TestApi.CQRS.Commands.Abstractions;

namespace TestApi.DataBase.CQRS.Commands
{
    public class WriteTokenCommandHandler: ICommandHandler<WriteTokenCommand>
    {
        private readonly ContextProvider _provider;

        public WriteTokenCommandHandler(ContextProvider provider)
        {
            _provider = provider;
        }

        public async Task HandleAsync(WriteTokenCommand handled)
        {
            using (var context = _provider.GetConnection())
            {
                await context.QueryAsync(
                    "SELECT funcs.write_token(@UserId, @Token, @Platform, @Expiry, @Creation, @Jwt)",
                    new
                    {
                        UserId = handled.UserId,
                        Token = handled.Token,
                        Platform = handled.Platform,
                        Expiry = handled.ExpiryDate,
                        Creation = DateTime.UtcNow,
                        Jwt = handled.JwtId,
                    });
            }
        }
    }
}
using System.Threading.Tasks;
using Dapper;
using TestApi.CQRS.Commands;
using TestApi.CQRS.Commands.Abstractions;

namespace TestApi.DataBase.CQRS.Commands
{
    public class ConfirmEmailCommandHandler : ICommandHandler<ConfirmEmailCommand>
    {
        private readonly ContextProvider _provider;

        public ConfirmEmailCommandHandler(ContextProvider provider)
        {
            _provider = provider;
        }

        public async Task HandleAsync(ConfirmEmailCommand handled)
        {
            using (var context = _provider.GetConnection())
            {
                await context.QueryAsync("SELECT funcs.activate(@Url)", new {Url = handled.Activation});
            }
        }
    }
}
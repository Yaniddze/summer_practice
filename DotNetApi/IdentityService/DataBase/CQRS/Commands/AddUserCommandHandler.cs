using System.Threading.Tasks;
using Dapper;
using TestApi.CQRS.Commands;
using TestApi.CQRS.Commands.Abstractions;

namespace TestApi.DataBase.CQRS
{
    public class AddUserCommandHandler : ICommandHandler<AddUserCommand>
    {
        private readonly ContextProvider _provider;

        public AddUserCommandHandler(ContextProvider provider)
        {
            _provider = provider;
        }

        public async Task HandleAsync(AddUserCommand handled)
        {
            using (var context = _provider.GetConnection())
            {
                await context.QueryAsync<bool>(
                    "SELECT funcs.register(@Id, @Login, @Password, @Email, @ActivationUrl);",
                    new
                    {
                        Id = handled.UserToAdd.Id,
                        Login = handled.UserToAdd.Login,
                        Password = handled.UserToAdd.Password,
                        Email = handled.UserToAdd.UserEmail.Email,
                        ActivationUrl = handled.UserToAdd.UserEmail.ActivationUrl,
                    });
            }
        }
    }
}
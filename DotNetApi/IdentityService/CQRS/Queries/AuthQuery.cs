using TestApi.CQRS.Queries.Abstractions;
using TestApi.Entities.Users;

namespace TestApi.CQRS.Queries
{
    public class AuthQuery: IQuery<User>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
using TestApi.CQRS.Queries.Abstractions;
using TestApi.Entities.Users;

namespace TestApi.CQRS.Queries
{
    public class UserByLoginQuery: IQuery<User>
    {
        public string Login { get; set; }
    }
}
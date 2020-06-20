using TestApi.CQRS.Queries.Abstractions;
using TestApi.Entities.Users;

namespace TestApi.CQRS.Queries
{
    public class UserByEmailQuery: IQuery<User>
    {
        public string Email { get; set; }
    }
}
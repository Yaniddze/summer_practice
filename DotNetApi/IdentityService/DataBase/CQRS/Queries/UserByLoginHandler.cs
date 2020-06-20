using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using TestApi.CQRS.Queries;
using TestApi.CQRS.Queries.Abstractions;
using TestApi.DataBase.Entities;
using TestApi.Entities.Users;

namespace TestApi.DataBase.CQRS.Queries
{
    public class UserByLoginHandler: IQueryHandler<UserByLoginQuery, User>
    {
        private readonly ContextProvider _provider;
        private readonly IMapper _mapper;

        public UserByLoginHandler(ContextProvider provider, IMapper mapper)
        {
            _provider = provider;
            _mapper = mapper;
        }

        public async Task<User> HandleAsync(UserByLoginQuery handled)
        {
            using (var context = _provider.GetConnection())
            {
                var founded = await context.QueryFirstOrDefaultAsync<UserDB>(
                    "SELECT * FROM tables.users WHERE login = @Login LIMIT 1",
                    handled);
                return founded is null ? null : _mapper.Map<UserDB, User>(founded);
            }
        }
    }
}
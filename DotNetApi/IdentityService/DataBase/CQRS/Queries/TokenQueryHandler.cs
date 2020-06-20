using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using TestApi.CQRS.Queries;
using TestApi.CQRS.Queries.Abstractions;
using TestApi.DataBase.Entities;
using TestApi.Entities.Tokens;

namespace TestApi.DataBase.CQRS.Queries
{
    public class TokenQueryHandler: IQueryHandler<TokenQuery, Token>
    {
        private readonly ContextProvider _provider;
        private readonly IMapper _mapper;

        public TokenQueryHandler(ContextProvider provider, IMapper mapper)
        {
            _provider = provider;
            _mapper = mapper;
        }

        public async Task<Token> HandleAsync(TokenQuery handled)
        {
            using (var context = _provider.GetConnection())
            {
                var founded = await context.QueryFirstOrDefaultAsync<TokenDB>(
                    "SELECT * FROM tables.tokens WHERE token = @Token LIMIT 1",
                    handled);
                return founded is null ? null : _mapper.Map<TokenDB, Token>(founded);
            }
        }
    }
}
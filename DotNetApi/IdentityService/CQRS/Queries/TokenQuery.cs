using TestApi.CQRS.Queries.Abstractions;
using TestApi.Entities.Tokens;

namespace TestApi.CQRS.Queries
{
    public class TokenQuery: IQuery<Token>
    {
        public string Token { get; set; }
    }
}
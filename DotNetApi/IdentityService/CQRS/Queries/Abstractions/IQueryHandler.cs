using System.Threading.Tasks;

namespace TestApi.CQRS.Queries.Abstractions
{
    public interface IQueryHandler<TQuery, TReturn>
        where TQuery: IQuery<TReturn>
    {
        Task<TReturn> HandleAsync(TQuery handled);
    }
}
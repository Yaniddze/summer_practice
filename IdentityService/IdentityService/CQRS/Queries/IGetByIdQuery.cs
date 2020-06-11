using System;
using System.Threading.Tasks;
using TestApi.Entities.Abstractions;

namespace TestApi.CQRS.Queries
{
    public interface IGetByIdQuery<TEntity> where TEntity: Entity
    {
        Task<TEntity> InvokeAsync(Guid id);
    }
}

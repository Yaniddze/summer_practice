using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StreamingService.Entities;

namespace StreamingService.Repositories
{
    public interface IRepository<TEntity> where TEntity: Entity
    {
        Task<TEntity> GetByTemplateAsync(Expression<Func<TEntity, bool>> pattern);
        Task InsertAsync(TEntity entity);
    }
}
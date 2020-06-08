using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StreamingApi.Entities;

namespace StreamingApi.Repositories
{
    public interface IRepository<TEntity> where TEntity: Entity
    {
        Task<TEntity> GetByTemplateAsync(Expression<Func<TEntity, bool>> pattern);
        Task InsertAsync(TEntity entity);
    }
}
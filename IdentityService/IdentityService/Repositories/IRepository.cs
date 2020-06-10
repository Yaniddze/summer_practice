using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestApi.Entities;
using TestApi.Entities.Abstractions;

namespace TestApi.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();        
        Task<IEnumerable<TEntity>> GetWithPatternAsync(Expression<Func<TEntity, bool>> pattern);        
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> FindOneWithPatternAsync(Expression<Func<TEntity, bool>> pattern);
        Task InsertAsync(TEntity entity);        
        Task DeleteAsync(Guid id);        
        Task UpdateAsync(TEntity entity);        
    }
}
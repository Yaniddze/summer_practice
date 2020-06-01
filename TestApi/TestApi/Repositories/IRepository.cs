using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.Entities;

namespace TestApi.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();        
        Task<IEnumerable<TEntity>> GetWithPatternAsync(Func<TEntity, bool> pattern);        
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> FindOneWithPatternAsync(Func<TEntity, bool> pattern);
        Task InsertAsync(TEntity entity);        
        Task DeleteAsync(Guid id);        
        Task UpdateAsync(TEntity entity);        
        Task SaveAsync(); 
    }
}
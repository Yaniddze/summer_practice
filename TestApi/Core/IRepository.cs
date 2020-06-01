using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();        
        Task<IEnumerable<TEntity>> GetWithPatternAsync(Func<TEntity, bool> pattern);        
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> FindOneWithPattern(Func<TEntity, bool> patter);
        Task InsertAsync(TEntity entity);        
        Task DeleteAsync(Guid id);        
        Task UpdateAsync(TEntity entity);        
        Task SaveAsync(); 
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetCustomersAsync();        
        TEntity GetByIdAsync(Guid id);        
        Task InsertCustomerAsync(TEntity entity);        
        Task DeleteCustomerAsync(Guid id);        
        Task UpdateCustomerAsync(TEntity entity);        
        Task SaveAsync(); 
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetCustomers();        
        TEntity GetById(Guid id);        
        Task InsertCustomer(TEntity entity);        
        Task DeleteCustomer(Guid id);        
        Task UpdateCustomer(TEntity entity);        
        Task Save(); 
    }
}
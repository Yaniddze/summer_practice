using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.Entities;

namespace TestApi.Repositories
{
    public class TokenRepository: IRepository<RefreshToken>
    {
        private static readonly List<RefreshToken> _tokens = new List<RefreshToken>();
        
        public Task<IEnumerable<RefreshToken>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RefreshToken>> GetWithPatternAsync(Func<RefreshToken, bool> pattern)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken> FindOneWithPatternAsync(Func<RefreshToken, bool> pattern)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(RefreshToken entity)
        {
            _tokens.Add(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(RefreshToken entity)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
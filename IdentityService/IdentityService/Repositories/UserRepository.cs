//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using IdentityService.Entities;
//
//namespace IdentityService.Repositories
//{
//    public class UserRepository: IRepository<User>
//    {
//        private static readonly List<User> _users = new List<User>();
//        public Task<IEnumerable<User>> GetAllAsync()
//        {
//            throw new NotImplementedException();
//        }
//
//        public Task<IEnumerable<User>> GetWithPatternAsync(Func<User, bool> pattern)
//        {
//            throw new NotImplementedException();
//        }
//
//        public Task<User> GetByIdAsync(Guid id)
//        {
//            return Task.FromResult<User>(_users.FirstOrDefault(x => x.Id == id));
//        }
//
//        public Task<User> FindOneWithPatternAsync(Func<User, bool> pattern)
//        {
//            return Task.FromResult(_users.FirstOrDefault(pattern));
//        }
//
//        public Task InsertAsync(User entity)
//        {
//            _users.Add(entity);
//            return Task.CompletedTask;
//        }
//
//        public Task DeleteAsync(Guid id)
//        {
//            throw new NotImplementedException();
//        }
//
//        public Task UpdateAsync(User entity)
//        {
//            var foundedIndex = _users.IndexOf(entity);
//            if (foundedIndex == -1)
//            {
//                _users.Add(entity);
//            }
//            else
//            {
//                _users[foundedIndex] = entity;
//            }
//
//            return Task.CompletedTask;
//        }
//
//        public Task SaveAsync()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
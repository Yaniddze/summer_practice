using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestApi.DataBase.Context;
using TestApi.DataBase.Entities;
using TestApi.Entities;
using TestApi.Entities.User;
using TestApi.Repositories;

namespace TestApi.DataBase.Repositories
{
    public class UsersRepository : IRepository<User>
    {
        private readonly IContextProvider _contextProvider;
        private readonly IMapper _mapper;

        public UsersRepository(IContextProvider contextProvider, IMapper mapper)
        {
            _contextProvider = contextProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (var context = _contextProvider.GetContext())
            {
                return await context.users.Select(x => _mapper.Map<UserDB, User>(x)).ToListAsync();
            }
        }

        public async Task<IEnumerable<User>> GetWithPatternAsync(Expression<Func<User, bool>> pattern)
        {
            using (var context = _contextProvider.GetContext())
            {
                var filter = _mapper.Map<Expression<Func<User, bool>>, Expression<Func<UserDB, bool>>>(pattern);

                var founded = await context.users
                    .Where(filter)
                    .Select(x => _mapper.Map<UserDB, User>(x))
                    .ToListAsync();
                return founded;
            }
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            using (var context = _contextProvider.GetContext())
            {
                var founded = await context.users.FirstOrDefaultAsync(x => x.id == id);
                return founded == null ? null : _mapper.Map<UserDB, User>(founded);
            }
        }

        public async Task<User> FindOneWithPatternAsync(Expression<Func<User, bool>> pattern)
        {
            using (var context = _contextProvider.GetContext())
            {
                var filter = _mapper.Map<Expression<Func<User, bool>>, Expression<Func<UserDB, bool>>>(pattern);
            
                var founded = await context.users
                    .FirstOrDefaultAsync(filter);
                return founded == null ? null : _mapper.Map<UserDB, User>(founded);
            }
        }

        public async Task InsertAsync(User entity)
        {
            using (var context = _contextProvider.GetContext())
            {
                var toInsert = _mapper.Map<User, UserDB>(entity);
                context.users.Add(toInsert);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var context = _contextProvider.GetContext())
            {
                var toDelete = await context.users.FirstOrDefaultAsync(x => x.id == id);
                if (toDelete != null)
                {
                    context.users.Remove(toDelete);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task UpdateAsync(User entity)
        {
            using (var context = _contextProvider.GetContext())
            {
                var founded = await context.users.FirstOrDefaultAsync(x => x.id == entity.Id);
                if (founded != null)
                {
                    founded.creation_date = entity.UserToken.CreationDate;
                    founded.expiry_date = entity.UserToken.ExpiryDate;
                    founded.jwtid = entity.UserToken.JwtId;
                    founded.token = entity.UserToken.Token;
                    founded.email = entity.UserEmail.Email;
                    founded.isemailconfirmed = entity.UserEmail.IsEmailConfirmed;
                    founded.login = entity.Login;
                    founded.password = entity.Password;
                    founded.activation_url = entity.UserEmail.ActivationUrl;

                    context.Entry(founded).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
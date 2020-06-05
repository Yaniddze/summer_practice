using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestApi.DataBase.Entities;
using TestApi.Entities;
using TestApi.Repositories;

namespace TestApi.DataBase.Repositories
{
    public class UsersRepository : IRepository<User>
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public UsersRepository(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.users.Select(x => _mapper.Map<UserDB, User>(x)).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetWithPatternAsync(Expression<Func<User, bool>> pattern)
        {
            var filter = _mapper.Map<Expression<Func<User, bool>>, Expression<Func<UserDB, bool>>>(pattern);

            var founded = await _context.users
                .Where(filter)
                .Select(x => _mapper.Map<UserDB, User>(x))
                .ToListAsync();
            return founded;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var founded = await _context.users.FirstOrDefaultAsync(x => x.id == id);
            return founded == null ? null : _mapper.Map<UserDB, User>(founded);
        }

        public async Task<User> FindOneWithPatternAsync(Expression<Func<User, bool>> pattern)
        {
            var filter = _mapper.Map<Expression<Func<User, bool>>, Expression<Func<UserDB, bool>>>(pattern);
            
            var founded = await _context.users
                .FirstOrDefaultAsync(filter);
            return founded == null ? null : _mapper.Map<UserDB, User>(founded);
        }

        public async Task InsertAsync(User entity)
        {
            var toInsert = _mapper.Map<User, UserDB>(entity);
            _context.users.Add(toInsert);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var toDelete = await _context.users.FirstOrDefaultAsync(x => x.id == id);
            if (toDelete != null)
            {
                _context.users.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(User entity)
        {
            var founded = await _context.users.FirstOrDefaultAsync(x => x.id == entity.Id);
            if (founded != null)
            {
                founded.creation_date = entity.UserToken.CreationDate;
                founded.expiry_date = entity.UserToken.ExpiryDate;
                founded.jwtid = entity.UserToken.JwtId;
                founded.token = entity.UserToken.Token;
                founded.email = entity.Email;
                founded.isemailconfirmed = entity.IsEmailConfirmed;
                founded.login = entity.Login;
                founded.password = entity.Password;
                founded.activation_url = entity.ActivationUrl;

                _context.Entry(founded).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
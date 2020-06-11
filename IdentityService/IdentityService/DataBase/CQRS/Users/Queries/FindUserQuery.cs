using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestApi.CQRS.Queries;
using TestApi.DataBase.Context;
using TestApi.DataBase.Entities;
using TestApi.Entities.User;

namespace TestApi.DataBase.CQRS.Users.Queries
{
    public class FindUserQuery: IFindQuery<User>
    {
        private readonly IContextProvider _contextProvider;
        private readonly IMapper _mapper;

        public FindUserQuery(IMapper mapper, IContextProvider contextProvider)
        {
            _mapper = mapper;
            _contextProvider = contextProvider;
        }

        public async Task<User> FindOneAsync(Expression<Func<User, bool>> pattern)
        {
            using (var context = _contextProvider.GetContext())
            {
                var mappedPattern = _mapper.Map<Expression<Func<User, bool>>, Expression<Func<UserDB, bool>>>(pattern);
                var founded = await context.users.FirstOrDefaultAsync(mappedPattern);

                return founded == null ? null : _mapper.Map<UserDB, User>(founded);
            }
        }

        public async Task<IEnumerable<User>> FindManyAsync(Expression<Func<User, bool>> pattern)
        {
            using (var context = _contextProvider.GetContext())
            {
                var mappedPattern = _mapper.Map<Expression<Func<User, bool>>, Expression<Func<UserDB, bool>>>(pattern);
                var founded = await context.users
                    .Where(mappedPattern)
                    .Select(x => _mapper.Map<UserDB, User>(x))
                    .ToListAsync();

                return founded;
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestApi.CQRS.Queries;
using TestApi.DataBase.Context;
using TestApi.DataBase.Entities;
using TestApi.Entities.User;

namespace TestApi.DataBase.CQRS.Users.Queries
{
    public class GetUserById: IGetByIdQuery<User>
    {
        private readonly IContextProvider _contextProvider;
        private readonly IMapper _mapper;

        public GetUserById(IContextProvider contextProvider, IMapper mapper)
        {
            _contextProvider = contextProvider;
            _mapper = mapper;
        }

        public async Task<User> InvokeAsync(Guid id)
        {
            using (var context = _contextProvider.GetContext())
            {
                var founded = await context.users.FirstOrDefaultAsync(x => x.id == id);

                return founded == null ? null : _mapper.Map<UserDB, User>(founded);
            }
        }
    }
}
using System.Threading.Tasks;
using AutoMapper;
using TestApi.CQRS.Commands;
using TestApi.DataBase.Context;
using TestApi.DataBase.Entities;
using TestApi.Entities.User;

namespace TestApi.DataBase.CQRS.Users.Commands.Add
{
    public class AddUserCommandHandler: ICommandHandler<AddUserCommand>
    {
        private readonly IContextProvider _contextProvider;
        private readonly IMapper _mapper;

        public AddUserCommandHandler(IMapper mapper, IContextProvider contextProvider)
        {
            _mapper = mapper;
            _contextProvider = contextProvider;
        }

        public async Task HandleAsync(AddUserCommand handled)
        {
            using (var context = _contextProvider.GetContext())
            {
                var mappedUser = _mapper.Map<User, UserDB>(handled.UserToAdd);

                context.users.Add(mappedUser);
                
                await context.SaveChangesAsync();
            }
        }
    }
}
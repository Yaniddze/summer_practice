using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestApi.CQRS.Commands;
using TestApi.DataBase.Context;
using TestApi.DataBase.Entities;
using Z.EntityFramework.Plus;

namespace TestApi.DataBase.CQRS.Users.Commands.Update.ConfirmEmail
{
    public class ConfirmEmailCommandHandler: ICommandHandler<ConfirmEmailCommand>
    {
        private readonly IContextProvider _contextProvider;
        private readonly IMapper _mapper;

        public ConfirmEmailCommandHandler(IMapper mapper, IContextProvider contextProvider)
        {
            _mapper = mapper;
            _contextProvider = contextProvider;
        }

        public async Task HandleAsync(ConfirmEmailCommand handled)
        {
            using (var context = _contextProvider.GetContext())
            {
                await context.users
                    .Where(x => x.id == handled.UserId)
                    .UpdateAsync(x => new UserDB{ isemailconfirmed = true });
            }
        }
    }
}
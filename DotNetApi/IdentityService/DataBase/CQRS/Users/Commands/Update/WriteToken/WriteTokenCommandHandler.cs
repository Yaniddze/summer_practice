using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestApi.CQRS.Commands;
using TestApi.DataBase.Context;
using TestApi.DataBase.Entities;
using Z.EntityFramework.Plus;

namespace TestApi.DataBase.CQRS.Users.Commands.Update.WriteToken
{
    public class WriteTokenCommandHandler: ICommandHandler<WriteTokenCommand>
    {
        private readonly IContextProvider _contextProvider;
        private readonly IMapper _mapper;

        public WriteTokenCommandHandler(IMapper mapper, IContextProvider contextProvider)
        {
            _mapper = mapper;
            _contextProvider = contextProvider;
        }

        public async Task HandleAsync(WriteTokenCommand handled)
        {
            using (var context = _contextProvider.GetContext())
            {
                await context.users
                    .Where(x => x.id == handled.UserId)
                    .UpdateAsync(x => new UserDB
                    {
                        jwtid = handled.NewToken.JwtId,
                        token = handled.NewToken.Token,
                        creation_date = handled.NewToken.CreationDate,
                        expiry_date = handled.NewToken.ExpiryDate,
                    });
            }
        }
    }
}
using MediatR;

namespace AspGateway.UseCases.GetMusic
{
    public class GetMusicRequest: IRequest<GetMusicAnswer>
    {
        public string MusicTitle { get; set; }
    }
}
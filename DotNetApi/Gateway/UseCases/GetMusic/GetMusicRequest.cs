using MediatR;

namespace Gateway.UseCases.GetMusic
{
    public class GetMusicRequest: IRequest<GetMusicAnswer>
    {
        public string MusicTitle { get; set; } = "";
    }
}
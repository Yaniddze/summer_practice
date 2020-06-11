using MediatR;

namespace StreamingService.UseCases.GetSong
{
    public class GetSongRequest: IRequest<GetSongAnswer>
    {
        public string musicTitle { get; set; }
    }
}
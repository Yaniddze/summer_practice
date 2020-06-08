using MediatR;

namespace StreamingApi.UseCases.GetSong
{
    public class GetSongRequest: IRequest<GetSongAnswer>
    {
        public string musicTitle { get; set; }
    }
}
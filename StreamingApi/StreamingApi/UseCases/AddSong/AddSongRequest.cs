using MediatR;

namespace StreamingApi.UseCases.AddSong
{
    public class AddSongRequest: IRequest<AddSongAnswer>
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Content { get; set; }
    }
}
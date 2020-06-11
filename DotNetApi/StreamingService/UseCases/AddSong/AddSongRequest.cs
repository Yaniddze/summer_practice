using MediatR;

namespace StreamingService.UseCases.AddSong
{
    public class AddSongRequest: IRequest<AddSongAnswer>
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Content { get; set; }
    }
}
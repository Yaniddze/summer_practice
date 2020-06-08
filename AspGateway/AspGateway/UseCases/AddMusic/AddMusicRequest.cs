using MediatR;

namespace AspGateway.UseCases.AddMusic
{
    public class AddMusicRequest: IRequest<AddMusicAnswer>
    {
        public string title { get; set; }
        public string artist { get; set; }
        public string content { get; set; }
    }
}
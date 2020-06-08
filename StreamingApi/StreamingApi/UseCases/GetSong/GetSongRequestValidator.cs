using FluentValidation;

namespace StreamingApi.UseCases.GetSong
{
    public class GetSongRequestValidator: AbstractValidator<GetSongRequest>
    {
        public GetSongRequestValidator()
        {
            RuleFor(x => x.musicTitle).NotNull();
        }
    }
}
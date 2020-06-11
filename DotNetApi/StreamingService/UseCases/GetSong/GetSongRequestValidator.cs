using FluentValidation;

namespace StreamingService.UseCases.GetSong
{
    public class GetSongRequestValidator: AbstractValidator<GetSongRequest>
    {
        public GetSongRequestValidator()
        {
            RuleFor(x => x.musicTitle).NotNull();
        }
    }
}
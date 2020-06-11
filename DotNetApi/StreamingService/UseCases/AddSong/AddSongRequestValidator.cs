using FluentValidation;

namespace StreamingService.UseCases.AddSong
{
    public class AddSongRequestValidator: AbstractValidator<AddSongRequest>
    {
        public AddSongRequestValidator()
        {
            RuleFor(x => x.Artist).NotNull();
            RuleFor(x => x.Content).NotNull();
            RuleFor(x => x.Title).NotNull();
        }
    }
}
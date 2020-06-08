using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using StreamingApi.Entities;
using StreamingApi.Repositories;

namespace StreamingApi.UseCases.GetSong
{
    public class GetSongUseCase: IRequestHandler<GetSongRequest, GetSongAnswer>
    {
        private readonly IRepository<Song> _repository;
        private readonly IValidator<GetSongRequest> _validator;

        public GetSongUseCase(IRepository<Song> repository, IValidator<GetSongRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<GetSongAnswer> Handle(GetSongRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new GetSongAnswer
                {
                    Success = false,
                    Errors = validationResult.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}").ToList(),
                };
            }
            var founded = await _repository.GetByTemplateAsync(song => song.Title.Contains(request.musicTitle));

            if (founded == null)
            {
                return new GetSongAnswer
                {
                    Success = false,
                    Errors = new List<string>{ "Song not found" }
                };
            }

            return new GetSongAnswer
            {
                Success = true,
                Content = founded.Content
            };
        }
    }
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using StreamingService.Entities;
using StreamingService.Repositories;

namespace StreamingService.UseCases.AddSong
{
    public class AddSongUseCase: IRequestHandler<AddSongRequest, AddSongAnswer>
    {
        private readonly IRepository<Song> _repository;
        private readonly IValidator<AddSongRequest> _validator;
        private readonly IMapper _mapper;

        public AddSongUseCase(IRepository<Song> repository, IMapper mapper, IValidator<AddSongRequest> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<AddSongAnswer> Handle(AddSongRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new AddSongAnswer()
                {
                    Success = false,
                    Errors = validationResult.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}").ToList(),
                };
            }
            
            var song = _mapper.Map<AddSongRequest, Song>(request);

            await _repository.InsertAsync(song);
            
            return new AddSongAnswer
            {
                Success = true,
            };
        }
    }
}
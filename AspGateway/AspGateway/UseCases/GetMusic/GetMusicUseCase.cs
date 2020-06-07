using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AspGateway.Entities;
using MediatR;

namespace AspGateway.UseCases.GetMusic
{
    public class GetMusicUseCase : IRequestHandler<GetMusicRequest, GetMusicAnswer>
    {
        private readonly Urls _urls;
        private readonly HttpClient _apiClient;

        public GetMusicUseCase(Urls urls, HttpClient apiClient)
        {
            _urls = urls;
            _apiClient = apiClient;
        }

        public async Task<GetMusicAnswer> Handle(GetMusicRequest request, CancellationToken cancellationToken)
        {
//            var content = new StringContent(
//                JsonConvert.SerializeObject(request), 
//                System.Text.Encoding.UTF8,
//                "application/json"
//            );
            var url = $"{_urls.Streaming}{Urls.StreamingRoutes.Music}?musicTitle={request.MusicTitle}";
            var apiResponse = await _apiClient.GetAsync(
                url, cancellationToken);

            apiResponse.EnsureSuccessStatusCode();

            var responseString = await apiResponse.Content.ReadAsByteArrayAsync();

            return new GetMusicAnswer(){Music = responseString};
        }
    }
}
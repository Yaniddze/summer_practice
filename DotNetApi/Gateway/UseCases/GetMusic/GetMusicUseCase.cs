using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Gateway.Entities;
using MediatR;
using Newtonsoft.Json;

namespace Gateway.UseCases.GetMusic
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
            var url = $"{_urls.Streaming}{Urls.StreamingRoutes.Get}?musicTitle={request.MusicTitle}";
            var apiResponse = await _apiClient.GetAsync(
                url, cancellationToken);

            apiResponse.EnsureSuccessStatusCode();

            var responseString = await apiResponse.Content.ReadAsStringAsync();

            // If song not found then will come { Success:false, Errors:["Song not found"], content:null }
            try
            {
                var result = JsonConvert.DeserializeObject<GetMusicAnswer>(responseString);
                return result;
            }
            catch (Exception)
            {
                return new GetMusicAnswer()
                {
                    Success = true,
                    Music = await apiResponse.Content.ReadAsByteArrayAsync(),
                };
            }
        }
    }
}
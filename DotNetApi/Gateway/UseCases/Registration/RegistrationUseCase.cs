using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Gateway.Entities;
using MediatR;
using Newtonsoft.Json;

namespace Gateway.UseCases.Registration
{
    public class RegistrationUseCase: IRequestHandler<RegistrationRequest, RegistrationAnswer>
    {
        private readonly HttpClient _apiClient;
        private readonly Urls _urls;

        public RegistrationUseCase(Urls urls, HttpClient apiClient)
        {
            _urls = urls;
            _apiClient = apiClient;
        }


        public async Task<RegistrationAnswer> Handle(RegistrationRequest request, CancellationToken cancellationToken)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(request), 
                System.Text.Encoding.UTF8,
                "application/json"
            );
            var url = $"{_urls.Identity}{Urls.IdentityRoutes.Register}";
            var apiResponse = await _apiClient.PostAsync(
                url,
                content, 
                cancellationToken
            );

            apiResponse.EnsureSuccessStatusCode();

            var responseString = await apiResponse.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<RegistrationAnswer>(responseString);
        }
    }
}

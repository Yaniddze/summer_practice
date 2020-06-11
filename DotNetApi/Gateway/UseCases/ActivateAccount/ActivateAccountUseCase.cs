using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Gateway.Entities;
using MediatR;
using Newtonsoft.Json;

namespace Gateway.UseCases.ActivateAccount
{
    public class ActivateAccountUseCase: IRequestHandler<ActivateRequest, ActivateAnswer>
    {
        private readonly Urls _urls;
        private readonly HttpClient _apiClient;

        public ActivateAccountUseCase(Urls urls, HttpClient apiClient)
        {
            _urls = urls;
            _apiClient = apiClient;
        }

        public async Task<ActivateAnswer> Handle(ActivateRequest request, CancellationToken cancellationToken)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(request), 
                System.Text.Encoding.UTF8,
                "application/json"
            );
            var url = $"{_urls.Identity}{Urls.IdentityRoutes.Activate}";
            var apiResponse = await _apiClient.PostAsync(
                url,
                content, 
                cancellationToken
            );

            apiResponse.EnsureSuccessStatusCode();

            var responseString = await apiResponse.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<ActivateAnswer>(responseString);
        }
    }
}
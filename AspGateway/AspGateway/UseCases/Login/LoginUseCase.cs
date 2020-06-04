using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AspGateway.Entities;
using MediatR;
using Newtonsoft.Json;

namespace AspGateway.UseCases.Login
{
    public class LoginUseCase : IRequestHandler<LoginRequest, LoginAnswer>
    {
        private readonly HttpClient _apiClient;
        private readonly Urls _urls;

        public LoginUseCase(HttpClient apiClient, Urls urls)
        {
            _apiClient = apiClient;
            _urls = urls;
        }

        public async Task<LoginAnswer> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(request), 
                System.Text.Encoding.UTF8,
                "application/json"
            );
            var url = $"{_urls.Identity}{Urls.IdentityRoutes.Login}";
            var apiResponse = await _apiClient.PostAsync(
                url,
                content, 
                cancellationToken
            );

            apiResponse.EnsureSuccessStatusCode();

            var responseString = await apiResponse.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<LoginAnswer>(responseString);
        }
    }
}
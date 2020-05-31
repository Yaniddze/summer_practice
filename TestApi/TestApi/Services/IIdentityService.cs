using System.Threading.Tasks;
using TestApi.Controllers.Contract.Responses;

namespace TestApi.Services
{
    public interface IIdentityService
    {
        Task<RegisterResponse> registerAsync(string requestEmail, string requestPassword);
        Task<LoginResponse> loginAsync(string requestEmail, string requestPassword);
        Task<RefreshTokenResponse> refreshTokenAsync(string requestToken, string requestRefreshToken);
    }
}
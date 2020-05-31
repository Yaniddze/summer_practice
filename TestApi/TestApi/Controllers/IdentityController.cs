using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApi.Controllers.Contract.Requests;
using TestApi.Services;

namespace TestApi.Controllers
{
    
    public class IdentityController: Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            this._identityService = identityService;
        }

        [HttpPost("v1/register")]
        public async Task<IActionResult> register([FromBody] RegistrationRequest request)
        {
            var registerResponse = await _identityService.registerAsync(request.email, request.password);
            
            return Ok(registerResponse);
        }
    }
}
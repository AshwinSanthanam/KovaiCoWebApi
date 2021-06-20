using Google.Apis.Auth;
using System.Threading.Tasks;

namespace KC.WebApi.Services
{
    public interface IExternalTokenValidationService
    {
        Task<GoogleJsonWebSignature.Payload> ValidateGoogleToken(string clientId, string idToken);
    }
}

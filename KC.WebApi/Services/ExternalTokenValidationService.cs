using Google.Apis.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KC.WebApi.Services
{
    public class ExternalTokenValidationService : IExternalTokenValidationService
    {
        public async Task<GoogleJsonWebSignature.Payload> ValidateGoogleToken(string clientId, string idToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { clientId }
            };
            return await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
        }
    }
}

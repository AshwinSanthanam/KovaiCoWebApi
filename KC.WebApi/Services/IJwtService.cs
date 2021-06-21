using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace KC.WebApi.Services
{
    public interface IJwtService
    {
        string GenerateJwt(IEnumerable<Claim> claims, DateTime expiry, string key);

        Dictionary<string, string> GetClaims(string token);
    }
}

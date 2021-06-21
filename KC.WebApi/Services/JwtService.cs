using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KC.WebApi.Services
{
    public class JwtService : IJwtService
    {
        public Dictionary<string, string> GetClaims(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            var claimsDictionary = new Dictionary<string, string>();
            foreach (var claim in jsonToken.Claims)
            {
                claimsDictionary.Add(claim.Type, claim.Value);
            }
            return claimsDictionary;
        }

        public string GenerateJwt(IEnumerable<Claim> claims, DateTime expiry, string key)
        {
            var keyBytes = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiry,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

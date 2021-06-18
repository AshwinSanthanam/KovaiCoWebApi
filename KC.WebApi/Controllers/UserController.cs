using KC.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KC.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IJwtService _jwtService;

        public UserController(IConfiguration config, IJwtService jwtService)
        {
            _config = config;
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok(GenerateJSONWebToken());
        }

        private string GenerateJSONWebToken()
        {
            var key = _config.GetSection("Jwt:Key").Value;
            var expiry = Convert.ToInt32(_config.GetSection("Jwt:Expiry").Value);
            
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, "Ashwin"));
            claims.Add(new Claim(ClaimTypes.Role, "user"));

            return _jwtService.GenerateJwt(claims, DateTime.Now.AddDays(expiry), key);
        }
    }
}

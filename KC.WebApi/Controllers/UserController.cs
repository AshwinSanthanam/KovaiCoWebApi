using KC.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;

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

        [Authorize]
        [HttpPost]
        public IActionResult AuthTest()
        {
            return Ok();
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

using KC.Base;
using KC.Base.TransientModels;
using KC.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KC.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IJwtService _jwtService;
        private readonly IRepository _repository;

        public UserController(IConfiguration config, IJwtService jwtService, IRepository repository)
        {
            _config = config;
            _jwtService = jwtService;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            var user = await _repository.InsertUser(new TransientUser 
            {
                Email = "as@123.cpm",
                Password = "123"
            });
            return Ok(user);
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

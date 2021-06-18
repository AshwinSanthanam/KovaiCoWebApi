using AutoMapper;
using KC.Base;
using KC.Base.Models;
using KC.Base.TransientModels;
using KC.WebApi.Models.User;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KC.WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly IConfiguration _config;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public UserService(IRepository repository, IConfiguration config, IJwtService jwtService, IMapper mapper)
        {
            _repository = repository;
            _config = config;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<User> CreateUser(CreateUserRequest request)
        {
            var transientUser = new TransientUser();
            _mapper.Map(request, transientUser);
            return await _repository.InsertUser(transientUser);
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

using AutoMapper;
using KC.Base;
using KC.Base.Models;
using KC.Base.Queries;
using KC.Base.TransientModels;
using KC.Base.Validators;
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
        private readonly IUserValidator _userValidator;
        private readonly IUserQueries _userQueries;
        private readonly IRoleQueries _roleQueries;
        private readonly IConfiguration _config;
        private readonly IJwtService _jwtService;
        private readonly IExternalTokenValidationService _externalTokenValidationService;
        private readonly IMapper _mapper;

        public UserService(IRepository repository, IConfiguration config, IJwtService jwtService, IMapper mapper, IUserValidator userValidator, IRoleQueries roleQueries, IUserQueries userQueries, IExternalTokenValidationService externalTokenValidationService)
        {
            _repository = repository;
            _config = config;
            _jwtService = jwtService;
            _mapper = mapper;
            _userValidator = userValidator;
            _roleQueries = roleQueries;
            _userQueries = userQueries;
            _externalTokenValidationService = externalTokenValidationService;
        }

        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request, string role)
        {
            var transientUser = _mapper.Map(request, new TransientUser());
            transientUser.RoleId = await _roleQueries.GetRoleId(role);
            await _userValidator.Validate(transientUser);
            var user = await _repository.InsertUser(transientUser);
            return _mapper.Map(user, new CreateUserResponse());
        }

        public async Task<string> Authenticate(AuthenticateUserRequest request, string role)
        {
            var user = await _userQueries.GetUser(request.Email, request.Password);
            if (user == null || user.Role.Name != role)
            {
                return null;
            }
            else
            {
                string jwt = GenerateJSONWebToken(user);
                return jwt;
            }
        }

        private string GenerateJSONWebToken(User user)
        {
            var key = _config.GetSection("Jwt:Key").Value;
            var expiry = Convert.ToInt32(_config.GetSection("Jwt:Expiry").Value);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));

            return _jwtService.GenerateJwt(claims, DateTime.Now.AddDays(expiry), key);
        }

        public async Task<string> AuthenticateExternalUser(AuthenticateExternalUserRequest request, string role)
        {
            var clientId = _config.GetSection("ExternalAuth:GoogleClientId").Value;
            var payload = await _externalTokenValidationService.ValidateGoogleToken(clientId, request.IdToken);
            var user = await _userQueries.GetUser(payload.Email);
            if(user == null)
            {
                var transientUser = new TransientUser
                {
                    Email = payload.Email,
                    IsSocialLogin = true,
                    Password = null,
                    RoleId = await _roleQueries.GetRoleId(role)
                };
                user = await _repository.InsertUser(transientUser);
            }
            return GenerateJSONWebToken(user);
        }
    }
}

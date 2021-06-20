using Google.Apis.Auth;
using KC.Base.Models;
using KC.Base.Validators;
using KC.WebApi.Models;
using KC.WebApi.Models.User;
using KC.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KC.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            return await Create(request, "user");
        }

        [HttpPost]
        [Route("admin")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateUserRequest request)
        {
            return await Create(request, "admin");
        }

        private async Task<IActionResult> Create(CreateUserRequest request, string role)
        {
            try
            {
                var createUserResponse = await _userService.CreateUser(request, role);
                var response = new GenericResponse<CreateUserResponse>
                {
                    IsSuccess = true,
                    Payload = createUserResponse
                };
                return Ok(response);
            }
            catch (DataIntegrityException ex)
            {
                var response = new GenericResponse<CreateUserResponse>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                return Conflict(response);
            }
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserRequest request)
        {
            return await Authenticate(request, "user");
        }

        [HttpPost]
        [Route("authenticate/admin")]
        public async Task<IActionResult> AuthenticateAdmin([FromBody] AuthenticateUserRequest request)
        {
            return await Authenticate(request, "admin");
        }

        private async Task<IActionResult> Authenticate(AuthenticateUserRequest request, string role)
        {
            string jwt = await _userService.Authenticate(request, role);
            if (string.IsNullOrEmpty(jwt))
            {
                var response = new GenericResponse<string>
                {
                    IsSuccess = false,
                    Message = "Invalid Credentials"
                };
                return Unauthorized(response);
            }
            else
            {
                var response = new GenericResponse<string>
                {
                    IsSuccess = true,
                    Payload = jwt
                };
                return Ok(response);
            }
        }

        [HttpPost]
        [Route("authenticate/external")]
        public async Task<IActionResult> AuthenticateExternalUser([FromBody] AuthenticateExternalUserRequest request)
        {
            return await AuthenticateExternal(request, "user");
        }

        [HttpPost]
        [Route("authenticate/external/admin")]
        public async Task<IActionResult> AuthenticateExternalAdmin([FromBody] AuthenticateExternalUserRequest request)
        {
            return await AuthenticateExternal(request, "admin");
        }

        private async Task<IActionResult> AuthenticateExternal(AuthenticateExternalUserRequest request, string role)
        {
            try
            {
                string jwt = await _userService.AuthenticateExternalUser(request, role);
                return Ok(new GenericResponse<string>
                {
                    IsSuccess = true,
                    Payload = jwt
                });
            }
            catch (Exception)
            {
                return Unauthorized(new GenericResponse<string>
                {
                    IsSuccess = false,
                    Message = "Invalid External Authentication"
                });
            }
        }
    }
}

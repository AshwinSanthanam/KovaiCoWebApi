using KC.Base.Validators;
using KC.WebApi.Models.User;
using KC.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var user = await _userService.CreateUser(request);
                return Ok(user);
            }
            catch(DataIntegrityException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}

using KC.Base.Models;
using KC.WebApi.Models;
using KC.WebApi.Models.Cart;
using KC.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KC.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart([FromBody] CartResource cartResource)
        {
            string email = GetEmail(HttpContext);
            await _cartService.CreateCart(cartResource, email);
            return Ok();
        }

        private string GetEmail(HttpContext httpContext)
        {
            StringValues authorizationToken;
            httpContext.Request.Headers.TryGetValue("Authorization", out authorizationToken);
            var jwt = authorizationToken.ToString().Split(' ')[1];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwt);
            var token = jsonToken as JwtSecurityToken;
            return token.Claims.First(x => x.Type == "email").Value;
        }
    }
}

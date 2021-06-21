using KC.WebApi.Models;
using KC.WebApi.Models.Cart;
using KC.WebApi.Models.Product;
using KC.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KC.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IJwtService _jwtService;

        public CartController(ICartService cartService, IJwtService jwtService)
        {
            _cartService = cartService;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateCart([FromBody] CartResource cartResource)
        {
            string email = GetEmail(HttpContext);
            await _cartService.CreateOrUpdateCart(cartResource, email);
            return Ok(new GenericResponse<object>
            {
                IsSuccess = true
            });
        }

        [HttpPost]
        [Route("change")]
        public async Task<IActionResult> AlterQuantity([FromBody] CartResource cartResource)
        {
            string email = GetEmail(HttpContext);
            await _cartService.AlterQuantity(cartResource, email);
            return Ok(new GenericResponse<object>
            {
                IsSuccess = true
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetCarts()
        {
            string email = GetEmail(HttpContext);
            var cartResources = await _cartService.GetProductsInActiveCart(email);
            return Ok(new GenericResponse<IEnumerable<CartResource>>
            {
                IsSuccess = true,
                Payload = cartResources
            });
        }

        private string GetEmail(HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue("Authorization", out StringValues authorizationToken);
            var jwt = authorizationToken.ToString().Split(' ')[1];
            var claims = _jwtService.GetClaims(jwt);
            return claims["email"];
        }
    }
}

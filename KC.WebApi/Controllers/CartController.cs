using KC.WebApi.Models;
using KC.WebApi.Models.Cart;
using KC.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
            return Ok(new GenericResponse<object>
            {
                IsSuccess = true
            });
        }

        [HttpDelete]
        [Route("product/{productId}")]
        public async Task<IActionResult> DeleteCart(long productId)
        {
            try
            {
                string email = GetEmail(HttpContext);
                await _cartService.DeleteCart(productId, email);
                return Ok(new GenericResponse<object> 
                {
                    IsSuccess = true
                });
            }
            catch (InvalidOperationException)
            {
                return NotFound(new GenericResponse<object>
                {
                    IsSuccess = false,
                    Message = "Invalid Product Id provided for delete"
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCarts()
        {
            string email = GetEmail(HttpContext);
            var cartIds = await _cartService.GetCarts(email);
            return Ok(new GenericResponse<IEnumerable<long>>
            {
                IsSuccess = true,
                Payload = cartIds
            });
        }

        private string GetEmail(HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue("Authorization", out StringValues authorizationToken);
            var jwt = authorizationToken.ToString().Split(' ')[1];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwt);
            var token = jsonToken as JwtSecurityToken;
            return token.Claims.First(x => x.Type == "email").Value;
        }
    }
}

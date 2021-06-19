using KC.WebApi.Models;
using KC.WebApi.Models.Product;
using KC.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KC.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateProduct([FromBody] CreateOrUpdateProductRequest request)
        {
            try
            {
                var response = await _productService.CreateOrUpdateProduct(request);
                return Ok(new GenericResponse<CreateOrUpdateProductResponse>
                {
                    IsSuccess = true,
                    Payload = response
                });
            }
            catch (InvalidOperationException)
            {
                return NotFound(new GenericResponse<CreateOrUpdateProductResponse>
                {
                    IsSuccess = false,
                    Message = "Invalid Product Id provided for update"
                });
            }
        }
    }
}

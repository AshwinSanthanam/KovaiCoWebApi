using KC.Base.Validators;
using KC.WebApi.Models;
using KC.WebApi.Models.Product;
using KC.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KC.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "admin")]
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
            catch (DataIntegrityException ex)
            {
                return Conflict(new GenericResponse<CreateOrUpdateProductResponse> 
                {
                    IsSuccess = false,
                    Message = ex.Message
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

        [Authorize(Roles = "admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            try
            {
                await _productService.DeleteProduct(id);
                return Ok(new GenericResponse<object>
                {
                    IsSuccess = true
                });
            }
            catch(InvalidOperationException)
            {
                return NotFound(new GenericResponse<CreateOrUpdateProductResponse>
                {
                    IsSuccess = false,
                    Message = "Invalid Product Id provided for delete"
                });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProducts(int pageSize, int offset, string productName)
        {
            return Ok(new GenericResponse<IEnumerable<GetProductResponse>> 
            {
                IsSuccess = true,
                Payload = await _productService.GetProducts(pageSize, offset, productName)
            });
        }
    }
}

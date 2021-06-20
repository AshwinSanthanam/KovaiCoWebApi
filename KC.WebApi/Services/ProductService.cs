using AutoMapper;
using KC.Base;
using KC.Base.Models;
using KC.Base.TransientModels;
using KC.Base.Validators;
using KC.WebApi.Models.Product;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KC.WebApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repository;
        private readonly IProductValidator _productValidator;
        private readonly IMapper _mapper;

        public ProductService(IRepository repository, IMapper mapper, IProductValidator productValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _productValidator = productValidator;
        }

        public async Task<CreateOrUpdateProductResponse> CreateOrUpdateProduct(CreateOrUpdateProductRequest request)
        {
            var transientProduct = _mapper.Map(request, new TransientProduct());
            
            Product product = null;
            if (request.Id.HasValue && request.Id.Value > 0)
            {
                if(await _repository.Products.AnyAsync(x => x.Name == transientProduct.Name && x.Id != request.Id))
                {
                    await _productValidator.Validate(transientProduct);
                }
                product = await _repository.UpdateProduct(request.Id.Value, transientProduct);
            }
            else
            {
                await _productValidator.Validate(transientProduct);
                product = await _repository.InsertProduct(transientProduct);
            }
            return _mapper.Map(product, new CreateOrUpdateProductResponse());
        }

        public async Task DeleteProduct(long id)
        {
            await _repository.DeleteProduct(id);
        }

        public async Task<IEnumerable<GetProductResponse>> GetProducts(int pageSize, int offset, string productName)
        {
            var products = _repository.Products.Where(x => x.IsActive);
            if(!string.IsNullOrEmpty(productName))
            {
                string productNameLowerCase = productName.ToLower();
                products = products.Where(x => x.Name.Contains(productNameLowerCase));
            }
            return await products.OrderBy(x => x.Name).Skip(offset * pageSize).Take(pageSize).
                Select(x => new GetProductResponse 
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    Name = x.Name,
                    Price = x.Price
                }).ToListAsync();
        }
    }
}

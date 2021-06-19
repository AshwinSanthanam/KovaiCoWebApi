using AutoMapper;
using KC.Base;
using KC.Base.Models;
using KC.Base.TransientModels;
using KC.WebApi.Models.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace KC.WebApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreateOrUpdateProductResponse> CreateOrUpdateProduct(CreateOrUpdateProductRequest request)
        {
            var transientProduct = _mapper.Map(request, new TransientProduct());
            Product product = null;
            if (request.Id.HasValue)
            {
                product = await _repository.UpdateProduct(request.Id.Value, transientProduct);
            }
            else
            {
                product = await _repository.InsertProduct(transientProduct);
            }
            return _mapper.Map(product, new CreateOrUpdateProductResponse());
        }
    }
}

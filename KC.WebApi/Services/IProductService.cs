using KC.Base.Models;
using KC.WebApi.Models.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KC.WebApi.Services
{
    public interface IProductService
    {
        Task<CreateOrUpdateProductResponse> CreateOrUpdateProduct(CreateOrUpdateProductRequest request);

        Task DeleteProduct(long id);

        Task<IEnumerable<Product>> GetProducts(int pageSize, int offset, string productName); 
    }
}

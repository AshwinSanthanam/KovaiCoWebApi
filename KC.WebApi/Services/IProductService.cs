using KC.WebApi.Models.Product;
using System.Threading.Tasks;

namespace KC.WebApi.Services
{
    public interface IProductService
    {
        Task<CreateOrUpdateProductResponse> CreateOrUpdateProduct(CreateOrUpdateProductRequest request);

        Task DeleteProduct(long id);
    }
}

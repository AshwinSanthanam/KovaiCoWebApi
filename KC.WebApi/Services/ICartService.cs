using KC.Base.Models;
using KC.WebApi.Models.Cart;
using KC.WebApi.Models.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KC.WebApi.Services
{
    public interface ICartService
    {
        Task<Cart> CreateOrUpdateCart(CartResource resource, string userEmail);

        Task<Cart> DeleteCart(long productId, string userEmail);

        Task<IEnumerable<GetProductResponse>> GetProductsInActiveCart(string userEmail);
    }
}

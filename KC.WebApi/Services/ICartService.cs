using KC.Base.Models;
using KC.WebApi.Models.Cart;
using System.Threading.Tasks;

namespace KC.WebApi.Services
{
    public interface ICartService
    {
        Task<CompleteCart> GetCompleteCart(string userEmail);

        Task<Cart> AlterQuantity(CartResource resource, string userEmail);
    }
}

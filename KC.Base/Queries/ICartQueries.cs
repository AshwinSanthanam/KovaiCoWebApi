using KC.Base.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KC.Base.Queries
{
    public interface ICartQueries
    {
        Task<IEnumerable<Cart>> GetAllActiveCarts(long userId);

        Task<Cart> GetActiveCart(long userId, long productId);
    }
}

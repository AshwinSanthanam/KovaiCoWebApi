using KC.Base;
using KC.Base.Models;
using KC.Base.Queries;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KC.DataAccess.Queries
{
    public class CartQueries : ICartQueries
    {
        private readonly IRepository _repository;

        public CartQueries(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Cart> GetActiveCart(long userId, long productId)
        {
            return await _repository.Carts.FirstAsync(x => x.IsActive && x.UserId == userId && x.ProductId == productId);
        }

        public async Task<IEnumerable<Cart>> GetAllActiveCarts(long userId)
        {
            return await _repository.Carts.Where(x => x.IsActive && x.UserId == userId).ToListAsync();
        }
    }
}

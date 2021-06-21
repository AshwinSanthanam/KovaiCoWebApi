using AutoMapper;
using KC.Base;
using KC.Base.Models;
using KC.Base.Queries;
using KC.Base.TransientModels;
using KC.WebApi.Models.Cart;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KC.WebApi.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserQueries _userQueries;

        public CartService(IRepository repository, IMapper mapper, IUserQueries userQueries)
        {
            _repository = repository;
            _mapper = mapper;
            _userQueries = userQueries;
        }

        public async Task<Cart> CreateCart(CartResource resource, string userEmail)
        {
            var transientCart = _mapper.Map(resource, new TransientCart());
            var user = await _userQueries.GetUser(userEmail);
            transientCart.UserId = user.Id;
            return await _repository.InsertCart(transientCart);
        }

        public async Task<Cart> DeleteCart(long productId, string userEmail)
        {
            var user = await _userQueries.GetUser(userEmail);
            var cart = await _repository.Carts.FirstAsync(x => x.UserId == user.Id && x.ProductId == productId);
            return await _repository.DeleteCart(cart.Id);
        }
    }
}

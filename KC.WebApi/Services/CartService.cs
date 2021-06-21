using AutoMapper;
using KC.Base;
using KC.Base.Models;
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

        public CartService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Cart> CreateCart(CartResource resource, string userEmail)
        {
            var transientCart = _mapper.Map(resource, new TransientCart());
            transientCart.UserId = (await _repository.Users.FirstAsync(x => x.Email == userEmail && x.IsActive)).Id;
            return await _repository.InsertCart(transientCart);
        }
    }
}

using AutoMapper;
using KC.Base;
using KC.Base.Models;
using KC.Base.Queries;
using KC.Base.TransientModels;
using KC.WebApi.Models.Cart;
using KC.WebApi.Models.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KC.WebApi.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserQueries _userQueries;
        private readonly ICartQueries _cartQueries;

        public CartService(IRepository repository, IMapper mapper, IUserQueries userQueries, ICartQueries cartQueries)
        {
            _repository = repository;
            _mapper = mapper;
            _userQueries = userQueries;
            _cartQueries = cartQueries;
        }

        public async Task<Cart> CreateOrUpdateCart(CartResource resource, string userEmail)
        {
            var transientCart = _mapper.Map(resource, new TransientCart());
            var user = await _userQueries.GetUser(userEmail);
            transientCart.UserId = user.Id;
            try
            {
                var cart = await _cartQueries.GetActiveCart(user.Id, resource.ProductId);
                if (transientCart.Quantity == 0)
                {
                    await _repository.DeleteCart(cart.Id);
                    return cart;
                }
                else
                {
                    return await _repository.UpdateCart(cart.Id, transientCart);
                }
            }
            catch (InvalidOperationException)
            {
                return await _repository.InsertCart(transientCart);
            }
        }

        public async Task<Cart> DeleteCart(long productId, string userEmail)
        {
            var user = await _userQueries.GetUser(userEmail);
            var cart = await _cartQueries.GetActiveCart(user.Id, productId);
            return await _repository.DeleteCart(cart.Id);
        }

        public async Task<IEnumerable<CartResource>> GetProductsInActiveCart(string userEmail)
        {
            var user = await _userQueries.GetUser(userEmail);
            var carts = (await _cartQueries.GetAllActiveCarts(user.Id)).Select(x => new CartResource 
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity
            });
            return carts;
        }
    }
}

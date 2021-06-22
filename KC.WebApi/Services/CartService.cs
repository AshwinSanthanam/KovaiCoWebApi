using AutoMapper;
using KC.Base;
using KC.Base.Models;
using KC.Base.Queries;
using KC.Base.TransientModels;
using KC.WebApi.Models.Cart;
using KC.WebApi.Models.Product;
using System;
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

        public async Task<Cart> AlterQuantity(CartResource resource, string userEmail)
        {
            var transientCart = _mapper.Map(resource, new TransientCart());
            var user = await _userQueries.GetUser(userEmail);
            transientCart.UserId = user.Id;
            try
            {
                var cart = await _cartQueries.GetActiveCart(user.Id, resource.ProductId);
                transientCart.Quantity += cart.Quantity;
                if (transientCart.Quantity <= 0)
                {
                    transientCart.Quantity = 0;
                    return await _repository.DeleteCart(cart.Id);
                }
                else
                {
                    return await _repository.UpdateCart(cart.Id, transientCart);
                }
            }
            catch (InvalidOperationException)
            {
                if (transientCart.Quantity <= 0)
                {
                    return null;
                }
                return await _repository.InsertCart(transientCart);
            }
        }

        public async Task<CompleteCart> GetCompleteCart(string userEmail)
        {
            var user = await _userQueries.GetUser(userEmail);
            var baseCart = await _cartQueries.GetAllActiveCarts(user.Id);
            var cartItems = baseCart.Select(x => new CartItem
            {
                Product = new GetProductResponse
                {
                    Id = x.Product.Id,
                    ImageUrl = x.Product.ImageUrl,
                    Name = x.Product.Name,
                    Price = x.Product.Price
                },
                Quantity = x.Quantity
            });
            var cart = new CompleteCart
            {
                CartItems = cartItems,
                TotalSum = cartItems.Sum(x => x.Quantity * x.Product.Price)
            };
            return cart;
        }
    }
}

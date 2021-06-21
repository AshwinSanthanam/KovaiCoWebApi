using KC.WebApi.Models.Product;
using System.Collections.Generic;

namespace KC.WebApi.Models.Cart
{
    public class CompleteCart
    {
        public IEnumerable<CartItem> CartItems { get; set; }
        public decimal TotalSum { get; set; }
    }

    public class CartItem
    {
        public GetProductResponse Product { get; set; }
        public int Quantity { get; set; }
    }
}

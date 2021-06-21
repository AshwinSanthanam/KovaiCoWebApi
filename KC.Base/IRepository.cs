using KC.Base.Models;
using KC.Base.TransientModels;
using System.Linq;
using System.Threading.Tasks;

namespace KC.Base
{
    public interface IRepository
    {
        IQueryable<User> Users { get; }
        IQueryable<Role> Roles { get; }
        IQueryable<Product> Products { get; }
        IQueryable<Cart> Carts { get; }

        Task<User> InsertUser(TransientUser transientUser);
        Task<Product> InsertProduct(TransientProduct transientProduct);
        Task<Cart> InsertCart(TransientCart transientCart);

        Task<User> UpdateUser(long id, TransientUser transientUser);
        Task<Product> UpdateProduct(long id, TransientProduct transientProduct);

        Task<User> DeleteUser(long id);
        Task<Product> DeleteProduct(long id);
        Task<Cart> DeleteCart(long id);
    }
}

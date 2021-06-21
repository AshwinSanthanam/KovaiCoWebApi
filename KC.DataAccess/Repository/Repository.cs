using KC.Base;
using KC.Base.Models;
using KC.Base.TransientModels;
using KC.DataAccess.MappingProfiles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KC.DataAccess.Repository
{
    public class Repository : IRepository
    {
        private readonly KovaiCoDbContext _dbContext;

        public Repository(KovaiCoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<User> Users => _dbContext.Users.Include(x => x.Role).AsQueryable();

        public IQueryable<Role> Roles => _dbContext.Roles.AsQueryable();

        public IQueryable<Product> Products => _dbContext.Products.AsQueryable();

        public IQueryable<Cart> Carts => _dbContext.Carts.Include(x => x.Product).Include(x => x.User).AsQueryable();

        public async Task<Cart> DeleteCart(long id)
        {
            var cartToDelete = await _dbContext.Carts.FirstAsync(x => x.Id == id);
            DeleteEntity(cartToDelete);
            _dbContext.Carts.Update(cartToDelete);
            await _dbContext.SaveChangesAsync();
            return cartToDelete;
        }

        public async Task<Product> DeleteProduct(long id)
        {
            var productToBeDeleted = await _dbContext.Products.FirstAsync(x => x.Id == id);
            DeleteEntity(productToBeDeleted);
            _dbContext.Products.Update(productToBeDeleted);
            await _dbContext.SaveChangesAsync();
            return productToBeDeleted;
        }

        public async Task<User> DeleteUser(long id)
        {
            var userToBeDeleted = await _dbContext.Users.FirstAsync(x => x.Id == id);
            userToBeDeleted.IsActive = false;
            userToBeDeleted.UpdatedOn = DateTimeOffset.Now;
            _dbContext.Users.Update(userToBeDeleted);
            await _dbContext.SaveChangesAsync();
            return userToBeDeleted;
        }

        public async Task<Cart> InsertCart(TransientCart transientCart)
        {
            var cart = AutoMapperConfiguration.Mapper.Map(transientCart, new Cart());
            InsertEntity(cart);
            _dbContext.Carts.Add(cart);
            await _dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<Product> InsertProduct(TransientProduct transientProduct)
        {
            var product = AutoMapperConfiguration.Mapper.Map(transientProduct, new Product());
            product.Name = product.Name.ToLower();
            InsertEntity(product);
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<User> InsertUser(TransientUser transientUser)
        {
            var user = new User
            {
                Email = transientUser.Email,
                Password = transientUser.Password,
                IsActive = true,
                RoleId = transientUser.RoleId,
                IsSocialLogin = transientUser.IsSocialLogin,
                CreatedOn = DateTimeOffset.Now
            };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<Cart> UpdateCart(long id, TransientCart transientCart)
        {
            var cartToBeUpdated = await _dbContext.Carts.FirstAsync(x => x.Id == id);
            AutoMapperConfiguration.Mapper.Map(transientCart, cartToBeUpdated);
            UpdateEntity(cartToBeUpdated);
            _dbContext.Carts.Update(cartToBeUpdated);
            await _dbContext.SaveChangesAsync();
            return cartToBeUpdated;
        }

        public async Task<Product> UpdateProduct(long id, TransientProduct transientProduct)
        {
            var productToBeUpdated = await _dbContext.Products.FirstAsync(x => x.Id == id);
            productToBeUpdated.Name = productToBeUpdated.Name.ToLower();
            AutoMapperConfiguration.Mapper.Map(transientProduct, productToBeUpdated);
            UpdateEntity(productToBeUpdated);
            _dbContext.Products.Update(productToBeUpdated);
            await _dbContext.SaveChangesAsync();
            return productToBeUpdated;
        }

        public async Task<User> UpdateUser(long id, TransientUser transientUser)
        {
            var userToBeUpdated = await _dbContext.Users.FirstAsync(x => x.Id == id);
            userToBeUpdated.Email = transientUser.Email;
            userToBeUpdated.Password = transientUser.Password;
            userToBeUpdated.IsSocialLogin = transientUser.IsSocialLogin;
            userToBeUpdated.UpdatedOn = DateTimeOffset.Now;
            _dbContext.Users.Update(userToBeUpdated);
            await _dbContext.SaveChangesAsync();
            return userToBeUpdated;
        }

        private void DeleteEntity(BaseEntity baseEntity)
        {
            baseEntity.IsActive = false;
            baseEntity.UpdatedOn = DateTimeOffset.Now;
        }

        private void InsertEntity(BaseEntity baseEntity)
        {
            baseEntity.IsActive = true;
            baseEntity.CreatedOn = DateTimeOffset.Now;
        }

        private void UpdateEntity(BaseEntity baseEntity)
        {
            baseEntity.UpdatedOn = DateTimeOffset.Now;
        }
    }
}

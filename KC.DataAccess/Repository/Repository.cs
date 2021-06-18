using KC.Base;
using KC.Base.Models;
using KC.Base.TransientModels;
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

        public IQueryable<User> Users => _dbContext.Users.AsQueryable();

        public async Task<User> DeleteUser(long id)
        {
            var userToBeDeleted = await _dbContext.Users.FirstAsync(x => x.Id == id);
            userToBeDeleted.IsActive = false;
            userToBeDeleted.UpdatedOn = DateTimeOffset.Now;
            _dbContext.Users.Update(userToBeDeleted);
            await _dbContext.SaveChangesAsync();
            return userToBeDeleted;
        }

        public async Task<User> InsertUser(TransientUser transientUser)
        {
            var user = new User
            {
                Email = transientUser.Email,
                Password = transientUser.Password,
                IsActive = true,
                CreatedOn = DateTimeOffset.Now
            };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(long id, TransientUser transientUser)
        {
            var userToBeUpdated = await _dbContext.Users.FirstAsync(x => x.Id == id);
            userToBeUpdated.Email = transientUser.Email;
            userToBeUpdated.Password = transientUser.Password;
            userToBeUpdated.UpdatedOn = DateTimeOffset.Now;
            _dbContext.Users.Update(userToBeUpdated);
            await _dbContext.SaveChangesAsync();
            return userToBeUpdated;
        }
    }
}

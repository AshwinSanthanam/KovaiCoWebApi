using KC.Base;
using KC.Base.Models;
using KC.Base.TransientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IQueryable<User> Users => throw new NotImplementedException();

        public User DeleteUser(long id)
        {
            throw new NotImplementedException();
        }

        public User InsertUser(TransientUser transientUser)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(long id, TransientUser transientUser)
        {
            throw new NotImplementedException();
        }
    }
}

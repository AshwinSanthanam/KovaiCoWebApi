using KC.Base.Models;
using KC.Base.TransientModels;
using System.Linq;

namespace KC.Base
{
    public interface IRepository
    {
        IQueryable<User> Users { get; }

        User InsertUser(TransientUser transientUser);

        User UpdateUser(long id, TransientUser transientUser);

        User DeleteUser(long id);
    }
}

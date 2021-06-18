using KC.Base.Models;
using KC.Base.TransientModels;
using System.Linq;
using System.Threading.Tasks;

namespace KC.Base
{
    public interface IRepository
    {
        IQueryable<User> Users { get; }

        Task<User> InsertUser(TransientUser transientUser);

        Task<User> UpdateUser(long id, TransientUser transientUser);

        Task<User> DeleteUser(long id);
    }
}

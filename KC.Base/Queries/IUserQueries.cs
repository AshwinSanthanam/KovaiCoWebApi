using KC.Base.Models;
using System.Threading.Tasks;

namespace KC.Base.Queries
{
    public interface IUserQueries
    {
        Task<User> GetUser(string email, string password);

        Task<User> GetUser(string email);
    }
}

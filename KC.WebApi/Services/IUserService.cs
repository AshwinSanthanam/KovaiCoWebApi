using KC.Base.Models;
using KC.WebApi.Models.User;
using System.Threading.Tasks;

namespace KC.WebApi.Services
{
    public interface IUserService
    {
        Task<User> CreateUser(CreateUserRequest request);
    }
}

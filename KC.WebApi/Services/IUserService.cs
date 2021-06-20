using KC.WebApi.Models.User;
using System.Threading.Tasks;

namespace KC.WebApi.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateUser(CreateUserRequest request);

        Task<string> Authenticate(AuthenticateUserRequest request, string role);

        Task<string> AuthenticateExternalUser(AuthenticateExternalUserRequest request, string role);
    }
}

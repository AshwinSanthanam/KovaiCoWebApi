using KC.Base;
using KC.Base.Models;
using KC.Base.Queries;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KC.DataAccess.Queries
{
    public class UserQueries : IUserQueries
    {
        private readonly IRepository _repository;

        public UserQueries(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> GetUser(string email, string password)
        {
            return await _repository.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }
    }
}

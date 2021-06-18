using KC.Base;
using KC.Base.Queries;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KC.DataAccess.Queries
{
    public class RoleQueries : IRoleQueries
    {
        private readonly IRepository _repository;

        public RoleQueries(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<long> GetRoleId(string name)
        {
            var role = await _repository.Roles.FirstAsync(x => x.Name == name);
            return role.Id;
        }
    }
}

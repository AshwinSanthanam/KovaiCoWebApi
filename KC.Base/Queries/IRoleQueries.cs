using System.Threading.Tasks;

namespace KC.Base.Queries
{
    public interface IRoleQueries
    {
        Task<long> GetRoleId(string name);
    }
}

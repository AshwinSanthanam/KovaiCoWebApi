using System.Threading.Tasks;

namespace KC.Base.Validators
{
    public interface IValidator<T>
    {
        Task Validate(T transientEntity);
    }
}

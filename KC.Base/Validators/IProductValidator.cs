using KC.Base.TransientModels;
using System.Threading.Tasks;

namespace KC.Base.Validators
{
    public interface IProductValidator : IValidator<TransientProduct>
    {
        Task ValidateUniqueName(TransientProduct transientProduct);
    }
}

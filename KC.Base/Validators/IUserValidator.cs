using KC.Base.TransientModels;
using System.Threading.Tasks;

namespace KC.Base.Validators
{
    public interface IUserValidator : IValidator<TransientUser>
    {
        void ValidateEmailPattern(TransientUser transientUser);

        Task ValidateUniqueEmail(TransientUser transientUser);

        void ValidateMandatoryPassword(TransientUser transientUser);

        Task ValidateRole(TransientUser transientUser);
    }
}

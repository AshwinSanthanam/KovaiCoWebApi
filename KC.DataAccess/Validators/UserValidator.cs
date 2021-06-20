using KC.Base;
using KC.Base.Models;
using KC.Base.TransientModels;
using KC.Base.Validators;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KC.DataAccess.Validators
{
    public class UserValidator : IUserValidator
    {
        private readonly IRepository _repository;

        public UserValidator(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Validate(TransientUser transientEntity)
        {
            ValidateEmailPattern(transientEntity);
            await ValidateUniqueEmail(transientEntity);
            ValidateMandatoryPassword(transientEntity);
            await ValidateRole(transientEntity);
        }

        public void ValidateEmailPattern(TransientUser transientUser)
        {
            var isValidEmail = Regex.IsMatch(transientUser.Email, @"^[a-z0-9]+[\.\-_]*@[a-z0-9]+\.[a-z0-9]+$");
            if (!isValidEmail)
            {
                throw new DataIntegrityException(nameof(User), nameof(User.Email), "Invalid Email");
            }
        }

        public void ValidateMandatoryPassword(TransientUser transientUser)
        {
            if (string.IsNullOrEmpty(transientUser.Password))
            {
                throw new DataIntegrityException(nameof(User), nameof(User.Password), "Password is Required");
            }
        }

        public async Task ValidateRole(TransientUser transientUser)
        {
            var doesRoleExist = await _repository.Roles.AnyAsync(x => x.Id == transientUser.RoleId);
            if(!doesRoleExist)
            {
                throw new DataIntegrityException(nameof(User), nameof(User.RoleId), "Invalid Role Id");
            }
        }

        public async Task ValidateUniqueEmail(TransientUser transientUser)
        {
            var doesEmailExist = await _repository.Users.AnyAsync(x => x.Email == transientUser.Email);
            if(doesEmailExist)
            {
                throw new DataIntegrityException(nameof(User), nameof(User.Email), "Email Already Exists");
            }
        }
    }
}

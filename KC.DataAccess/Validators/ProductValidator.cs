using KC.Base;
using KC.Base.Models;
using KC.Base.TransientModels;
using KC.Base.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KC.DataAccess.Validators
{
    public class ProductValidator : IProductValidator
    {
        private readonly IRepository _repository;

        public ProductValidator(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Validate(TransientProduct transientEntity)
        {
            await ValidateUniqueName(transientEntity);
        }

        public async Task ValidateUniqueName(TransientProduct transientProduct)
        {
            if(await _repository.Products.AnyAsync(x => x.Name == transientProduct.Name))
            {
                throw new DataIntegrityException(nameof(Product), nameof(Product.Name), "Product Name must be Unique");
            }
        }
    }
}

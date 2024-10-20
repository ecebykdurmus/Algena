using Algena.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator() 
        {
            RuleFor(x => x.Price).GreaterThanOrEqualTo(1);
            RuleFor(x => x.StockAmount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.ProductName).NotEmpty();

        }
    }
}

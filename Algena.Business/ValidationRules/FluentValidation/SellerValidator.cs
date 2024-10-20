using Algena.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Business.ValidationRules.FluentValidation
{
    public class SellerValidator : AbstractValidator<Seller>
    {
        public SellerValidator() 
        {
            RuleFor(x => x.CompanyName).NotEmpty();
            RuleFor(x => x.Address).MaximumLength(150);

        }
    }
}

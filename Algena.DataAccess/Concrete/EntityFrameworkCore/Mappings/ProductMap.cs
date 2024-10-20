using Algena.Core.DataAccess.EntityFrameworkCore.Mappings;
using Algena.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class ProductMap : EntityMap<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductName).IsRequired();

            builder.HasMany(x => x.OrderDetails).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
            base.Configure(builder);
        }

    }
}

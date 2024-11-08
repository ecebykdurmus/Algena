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
    public class SellerMap : EntityMap<Seller>
    {
        public override void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CompanyName).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(150);

            builder.HasOne(x => x.AppUser).WithOne(x => x.Seller);

            //1-n ilişki var.
            //builder.HasMany(x => x.Orders).WithOne(x => x.Seller).HasForeignKey(x => x.SellerId);
            base.Configure(builder);

        }
    }
}

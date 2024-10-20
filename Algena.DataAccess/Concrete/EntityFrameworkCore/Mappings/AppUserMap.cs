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
    public class AppUserMap : EntityMap<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Customer).WithOne(x => x.AppUser).HasForeignKey<Customer>(x => x.Id); //AppUser'in Id'si.
            builder.HasOne(x => x.Seller).WithOne(x => x.AppUser).HasForeignKey<Seller>(x => x.Id);

            base.Configure(builder);
        }

    }
}

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
    public class CustomerMap : EntityMap<Customer>
    {
        //Core'deki EntityMap'den miras aldık.
        //override configure'u kullandık.
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            //Primary keyini belirttik.
            builder.HasKey(x => x.Id);

            //Boş geçilemez demek.
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();

            builder.Property(x => x.Address).HasMaxLength(150);
            builder.Property(x => x.Fax).HasMaxLength(14);

            //1-1 ilişkileri var.
            builder.HasOne(x => x.AppUser).WithOne(x => x.Customer);
            //builder.HasOne(x => x.AppUser).WithOne(x => x.Customer).HasForeignKey<AppUser>(x => x.Id); //Customer'in Id'si

            //1-n ilişkileri var.
            builder.HasMany(x => x.Orders).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId);

            base.Configure(builder);
        }
    }
}

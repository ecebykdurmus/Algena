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
    public class OrderDetailMap : EntityMap<OrderDetail>
    {
        public override void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(x => new { x.OrderId, x.ProductId });
            base.Configure(builder);
        }

    }
}

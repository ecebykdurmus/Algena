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
    public class CategoryMap : EntityMap<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CategoryName).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(50);

            builder.HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);

            base.Configure(builder);
        }
    }
}

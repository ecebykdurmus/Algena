using Algena.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Core.DataAccess.EntityFrameworkCore.Mappings
{
    public class EntityMap<T> : IEntityTypeConfiguration<T> where T : class, IEntity, new()
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            
        }
    }
}

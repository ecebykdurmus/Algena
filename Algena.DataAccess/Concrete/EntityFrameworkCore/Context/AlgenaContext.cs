using Algena.DataAccess.Concrete.EntityFrameworkCore.Mappings;
using Algena.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Algena.DataAccess.Concrete.EntityFrameworkCore.Context
{
    public class AlgenaContext : IdentityDbContext<AppUser,AppRole,int>
    {
        //public AlgenaContext(DbContextOptions<AlgenaContext> options) : base(options)
        //{

        //}

        public AlgenaContext(DbContextOptions<AlgenaContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //   optionsBuilder.UseSqlServer("Data Source=LAPTOP-D2KS16I5;Initial Catalog=AlgenaMyProject;Integrated Security=true;TrustServerCertificate=true;");
        //    base.OnConfiguring(optionsBuilder);
        //}

        


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new CustomerMap());
            builder.ApplyConfiguration(new OrderDetailMap());
            builder.ApplyConfiguration(new OrderMap());
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new SellerMap());

            builder.ApplyConfiguration(new AppUserMap()); 

            base.OnModelCreating(builder);
        }

        //Nesnelerimiz;
        DbSet<Category> Categories { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Seller> Sellers { get; set;}
    }
}

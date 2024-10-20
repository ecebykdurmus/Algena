using Algena.Business.DependencyResolvers.AutoFac;
using Algena.DataAccess.Concrete.EntityFrameworkCore.Context;
using Algena.Entities.Concrete;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Algena.Business.DependencyResolvers.Extention;
using Algena.WebUI.BasketTransaction;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBasketTransaction, BasketTransaction>();


//AutoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new BusinessModule()));

//AddDbContext
builder.Services.Register();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AlgenaContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddIdentity<AppUser, AppRole>(
      x => x.Password = new PasswordOptions()
      {
          RequiredLength = 0,
          RequiredUniqueChars = 0,
          RequireLowercase = false,
          RequireUppercase = false,
          RequireNonAlphanumeric = false,
          RequireDigit = false
      }
    ).AddEntityFrameworkStores<AlgenaContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using Algena.Business.DependencyResolvers.AutoFac;
using Algena.Business.DependencyResolvers.Extention;
using Algena.DataAccess.Concrete.EntityFrameworkCore.Context;
using Algena.Entities.Concrete;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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


builder.Services.AddAuthentication( x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer( x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = "localhost",
        ValidIssuer = "localhost",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("keykullaniyorumburadaasdasdfhashgdvasd"))
    };
});



builder.Services.AddCors( options => 
{
    options.AddPolicy("guvenlik", x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("guvenlik");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

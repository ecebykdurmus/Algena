using Algena.Business.Abstract;
using Algena.Business.DependencyResolvers.AutoFac;
using Algena.Business.DependencyResolvers.Extention;
using Algena.DataAccess.Concrete.EntityFrameworkCore.Context;
using Algena.Entities.Concrete;
using Algena.Entities.Dtos.CategoryDtos;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddCors(options =>
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

app.UseAuthorization();

app.MapControllers();



app.UseCors("guvenlik");



//Controller'e gerek kalmadan kýsaca halletmek için;

//GetAll
app.MapGet("/Categories", async ([FromServices] ICategoryService _categoryService) =>
{
    List<CategoryDto> categoryDtos = await _categoryService.GetAllAsync();
    return Results.Ok(categoryDtos);
});

//Get
app.MapGet("/Categories/{id}", async (int id, [FromServices]ICategoryService _categoryService) =>
{
    CategoryDto categoryDto = await _categoryService.GetByIdAsync(id);
    return Results.Ok(categoryDto);
});

//Add
app.MapPost("/Categories/Add", async (CategoryAddDto categoryDto, [FromServices]ICategoryService _categoryService) =>
{
    int result = await _categoryService.AddAsync(categoryDto);
    return Results.Ok(result);
});

//Delete
app.MapDelete("/Categories/Delete/{id}", async (int id, [FromServices]ICategoryService _categoryService) =>
{
    int result = await _categoryService.DeleteAsync(id);
    return Results.Ok(result);
});

//Update
app.MapPut("/Categories/Update", async (CategoryDto categoryDto, [FromServices]ICategoryService _categoryService) =>
{
    int result = await _categoryService.UpdateAsync(categoryDto);
    return Results.Ok(result);
});




app.Run();

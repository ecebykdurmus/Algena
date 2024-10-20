using Algena.DataAccess.Abstract;
using Algena.DataAccess.Concrete.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Algena.Business.DependencyResolvers.Extention
{
    public static class Container
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            services.AddScoped<IProductDal,ProductDal>();
            services.AddScoped<ICategoryDal,CategoryDal>();
            services.AddScoped<ICustomerDal,CustomerDal>();
            services.AddScoped<IOrderDal,OrderDal>();
            services.AddScoped<IOrderDetailDal,OrderDetailDal>();
            services.AddScoped<ISellerDal,SellerDal>();

            return services;
        }
    }
}

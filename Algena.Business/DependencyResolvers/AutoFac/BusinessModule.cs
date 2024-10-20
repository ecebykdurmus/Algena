using Algena.Business.Abstract;
using Algena.Business.Concrete;
using Autofac;


namespace Algena.Business.DependencyResolvers.AutoFac
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Servicelerimizi burada aldık.
            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<OrderProcessManager>().As<IOrderProcessService>();
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<AuthManager>().As<IAuthService>();

            base.Load(builder);
        }
    }
}

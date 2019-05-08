using Autofac;
using Autofac.Integration.WebApi;
using BLL.AutofacRegistration;
using BLL.Interfaces;
using BLL.Services;
using DAL.AutofacRegistration;
using System.Reflection;

namespace WebAPI.App_Start
{
    public class AutofacConfig
    {
        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule(new AutofacBLLRegistration());
            builder.RegisterModule(new AutofacDALRegistration());
            return builder.Build();
        }
    }
}
using Autofac;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Interfaces.Repositories;
using DAL.Repositories;

namespace DAL.AutofacRegistration
{
    public class AutofacDALRegistration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>()
               .As<IApplicationDbContext>()
               .InstancePerRequest();

            builder.RegisterType<TestRepository>()
                .As<ITestRepository>()
                .InstancePerRequest();

            builder.RegisterType<AdminRepository>()
                .As<IAdminRepository>()
                .InstancePerRequest();

            builder.RegisterType<UrlRepository>()
                .As<IUrlRepository>()
                .InstancePerRequest();

            builder.RegisterType<RefreshTokenRepository>()
                .As<IRefreshTokenRepository>()
                .InstancePerRequest();

            builder.RegisterType<QuestionRepository>()
                .As<IQuestionRepository>()
                .InstancePerRequest();

            builder.RegisterType<OptionRepository>()
                .As<IOptionRepository>()
                .InstancePerRequest();

            builder.RegisterType<TestResultRepository>()
                .As<ITestResultRepository>()
                .InstancePerRequest();

            builder.RegisterType<ClientRepository>()
                .As<IClientRepository>()
                .InstancePerRequest();

            builder.RegisterType<AnswerRepository>()
                .As<IAnswerRepository>()
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(GenericRepository<,>))
                .As(typeof(IGenericRepository<,>))
                .InstancePerRequest();
        }
    }
}

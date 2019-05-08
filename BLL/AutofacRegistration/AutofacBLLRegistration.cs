using Autofac;
using BLL.Interfaces;
using BLL.Services;
using NLog;

namespace BLL.AutofacRegistration
{
    public class AutofacBLLRegistration: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TestService>()
                .As<ITestService>()
                .InstancePerRequest();

            builder.RegisterType<AdminService>()
                .As<IAdminService>()
                .InstancePerRequest();

            builder.RegisterType<AuthService>()
                .As<IAuthService>()
                .InstancePerRequest();

            builder.RegisterType<QuestionService>()
                .As<IQuestionService>()
                .InstancePerRequest();

            builder.RegisterType<OptionService>()
                .As<IOptionService>()
                .InstancePerRequest();

            builder.RegisterType<UrlService>()
                .As<IUrlService>()
                .InstancePerRequest();

            builder.RegisterType<TestResultService>()
                .As<ITestResultService>()
                .InstancePerRequest();

            builder.RegisterType<AnswerService>()
                .As<IAnswerService>()
                .InstancePerRequest();

            builder.RegisterInstance(LogManager.GetCurrentClassLogger())
                .As<ILogger>()
                .SingleInstance();
        }
    }
}

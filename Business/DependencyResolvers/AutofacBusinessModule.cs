using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using FluentValidation;
using MediatR;

namespace Business.DependencyResolvers
{
    public class AutofacBusinessModule : Module
    {
        private readonly ConfigurationManager _configuration;

        /// <summary>
        /// for Autofac.
        /// </summary>
        public AutofacBusinessModule()
        {
        }

        public AutofacBusinessModule(ConfigurationManager configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();


            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()

                    .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .AsClosedTypesOf(typeof(IValidator<>));

            switch (_configuration.Mode)
            {
                case ApplicationMode.Development:
                    builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                            .Where(t => t.FullName.StartsWith("Business.Fakes"))
                            ;
                    break;
                case ApplicationMode.Profiling:

                    builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                            .Where(t => t.FullName.StartsWith("Business.Fakes.SmsService"));
                    break;
                case ApplicationMode.Staging:

                    builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                            .Where(t => t.FullName.StartsWith("Business.Fakes.SmsService"));
                    break;
                case ApplicationMode.Production:

                    builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                                    .Where(t => t.FullName.StartsWith("Business.Adapters"))
                                    ;
                    break;
                default:
                    break;
            }
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                            {
                                Selector = new AspectInterceptorSelector()
                            }).SingleInstance().InstancePerDependency();
        }
    }
}


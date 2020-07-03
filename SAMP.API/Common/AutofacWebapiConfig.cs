using Autofac;
using Autofac.Integration.WebApi;
using SAMP.BAL;
using SAMP.DAL;
using System.Reflection;
using System.Web.Http;

namespace SAMP.API.Common
{
    public class AutofacWebapiConfig
    {
        /// <summary>
        /// The container
        /// </summary>
        private static IContainer container;

        /// <summary>
        /// Initializes the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        /// <summary>
        /// Initializes the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="container">The container.</param>
        public static void Initialize(HttpConfiguration config, Autofac.IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>IContainer.</returns>
        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            ////Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<LoginCommands>()
                   .As<ILoginCommands>()
                   .InstancePerRequest();

            builder.RegisterType<LoginService>()
                   .As<ILoginService>()
                   .InstancePerRequest();

            builder.RegisterType<SOWMasterCommands>()
           .As<ISOWMasterCommands>()
           .InstancePerRequest();

            builder.RegisterType<SOWMasterService>()
                   .As<ISOWMasterService>()
                   .InstancePerRequest();

            ////Set the dependency resolver to be Autofac.
            container = builder.Build();

            return container;
        }
    }
}
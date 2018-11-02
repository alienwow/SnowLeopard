using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SnowLeopard.DependencyInjection
{
    /// <summary>
    /// Autofac Extension
    /// </summary>
    public static class AutofacExtension
    {
        /// <summary>
        /// Add SnowLeopard Autofac
        /// </summary>
        /// <param name="services"></param>
        public static IServiceProvider AddSnowLeopardAutofac(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);

            var serviceProvider = new AutofacServiceProvider(builder.Build());
            GlobalServices.SetServiceProvider(serviceProvider);

            return serviceProvider;
        }

    }
}

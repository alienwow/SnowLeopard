using Lynx.Extension;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;

namespace SnowLeopard.DependencyInjection
{
    /// <summary>
    /// Swagger Extension
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// AddSnowLeopardSwaggerGen
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="hostingEnvironment">hostingEnvironment</param>
        /// <param name="setupAction">setupAction</param>
        public static IServiceCollection AddSnowLeopardSwaggerGen(this IServiceCollection services
            , IHostingEnvironment hostingEnvironment
            , Action<SwaggerGenOptions> setupAction = null)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();

                if (File.Exists($"{hostingEnvironment.ContentRootPath}//{hostingEnvironment.ApplicationName}.xml"))
                {
                    // 包含 XML 文档
                    options.IncludeXmlComments($"{hostingEnvironment.ContentRootPath}//{hostingEnvironment.ApplicationName}.xml");
                }

                var schema = new Schema()
                {
                    Type = "number",
                    Format = "long",
                    Example = DateTime.Now.ToUnixTimestamp()
                };
                options.MapType<DateTime>(() => schema);
                options.MapType<DateTime?>(() => schema);

                if (setupAction != null) setupAction.Invoke(options);
            });

            return services;
        }

    }
}

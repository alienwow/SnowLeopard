using Lynx.Extension;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

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

                //获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径
                var appAssembly = Assembly.Load(new AssemblyName(hostingEnvironment.ApplicationName));
                var basePath = Path.GetDirectoryName(appAssembly.Location);
                var xmlPath = Path.Combine(basePath, $"{hostingEnvironment.ApplicationName}.xml");

                if (File.Exists(xmlPath))
                {
                    // 包含 XML 文档
                    options.IncludeXmlComments(xmlPath);
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

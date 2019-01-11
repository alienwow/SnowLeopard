using SnowLeopard.Lynx.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SnowLeopard.Infrastructure.Swagger;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        /// <summary>
        /// AddFileUploadFilter
        /// </summary>
        /// <param name="options"></param>
        public static void AddFileUploadFilter(this SwaggerGenOptions options)
        {
            options.OperationFilter<SwaggerFileUploadFilter>();
        }

        /// <summary>
        /// AddOAuth2BearerAuthentication
        /// </summary>
        /// <param name="options"></param>
        public static void AddOAuth2BearerAuthentication(this SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("Bearer", new ApiKeyScheme
            {
                Description = "JWT Bearer 授权 \"Authorization:     Bearer+空格+token\"",
                Name = "Authorization",
                In = "header",
                Type = "apiKey"
            });
            
            options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>()
            {
                { "Bearer",Enumerable.Empty<string>() }
            });
        }
    }
}

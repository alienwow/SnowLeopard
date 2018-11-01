using Exceptionless;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SnowLeopard.DependencyInjection;
using SnowLeopard.Infrastructure.Http;
using SnowLeopard.Mongo;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace SnowLeopard.WebApi
{
    public class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration">configuration</param>
        /// <param name="hostingEnvironment">hostingEnvironment</param>
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            GlobalConfig = Configuration.Get<GlobalConfig>();
            HostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// HostingEnvironment
        /// </summary>
        public IHostingEnvironment HostingEnvironment { get; }

        /// <summary>
        /// GlobalConfig
        /// </summary>
        public GlobalConfig GlobalConfig { get; }

        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddSnowLeopardMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSnowLeopardSwaggerGen(HostingEnvironment, options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "SnowLeopard.WebApi HTTP API",
                    Version = "v1",
                    Description = "The SnowLeopard.WebApi Service HTTP API",
                    TermsOfService = "None"
                });

                // 添加文件上传
                options.AddFileUploadFilter();

                // 为swagger 添加oauth2.0 bearer授权
                options.AddOAuth2BearerAuthentication();

            });

            services.Configure<GlobalConfig>(Configuration);

            // 添加服务发现组件
            services.AddSnowLeopardServiceDiscovery();

            // 添加所有 IDependencyTransientRegister 服务
            services.AddSnowLeopardServices();
            services.AddHttpClient();
            services.AddSingleton<SnowLeopardHttpClient, SnowLeopardHttpClient>();
            services.AddSnowLeopardRedis(Configuration);

            services.AddSnowLeopardMongoContext();

            return services.AddSnowLeopardAutofac();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory
        )
        {
            app.UseExceptionless(Configuration);
            loggerFactory.AddExceptionless();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", "SnowLeopard.WebApi.API V1");
                   c.OAuthClientId("snowleopard.webapiswaggerui");
                   c.OAuthAppName("SnowLeopard.WebApi Swagger UI");
               });

#if RELEASE
            app.RegisterService(GlobalConsts.SERVICE_NAME, GlobalConfig.ApplicationUrl);
#endif
        }
    }
}

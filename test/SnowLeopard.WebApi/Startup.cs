using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SnowLeopard.DependencyInjection;
using SnowLeopard.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace SnowLeopard.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var globalConfig = Configuration.Get<GlobalConfig>();

            services
                .AddMvc(options =>
                {
                    options.AddSnowLeopardFilters();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddControllersAsServices()
                .AddJsonOptions(options =>// 全局配置Json序列化处理
                {
                    //忽略循环引用
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //将时间转换为时间戳
                    options.SerializerSettings.ContractResolver = new DateTimeContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()// 使用驼峰样式
                    };
                });

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "SnowLeopard.WebApi HTTP API",
                    Version = "v1",
                    Description = "The Basket Service HTTP API",
                    TermsOfService = "Terms Of Service"
                });

                //options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                //{
                //    Type = "oauth2",
                //    Flow = "implicit",
                //    AuthorizationUrl = $"{Configuration.GetValue<string>("IdentityUrlExternal")}/connect/authorize",
                //    TokenUrl = $"{Configuration.GetValue<string>("IdentityUrlExternal")}/connect/token",
                //    Scopes = new Dictionary<string, string>()
                //    {
                //        { "basket", "Basket API" }
                //    }
                //});

                //options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            services.Configure<GlobalConfig>(Configuration);
            services.AddSnowLeopardServices();

            var container = new ContainerBuilder();
            container.Populate(services);

            var serviceProvider = new AutofacServiceProvider(container.Build());
            GlobalServices.SetServiceProvider(serviceProvider);

            return serviceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            var pathBase = string.Empty;
            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint($"{(!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty)}/swagger/v1/swagger.json", "SnowLeopard.WebApi.API V1");
                   c.OAuthClientId("snowleopard.webapiswaggerui");
                   c.OAuthAppName("SnowLeopard.WebApi Swagger UI");
               });

        }
    }
}

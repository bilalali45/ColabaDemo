using ElmahCore.Mvc;
using ElmahCore.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using ByteWebConnector.SDK.Abstraction;
using ByteWebConnector.SDK.Mismo;
using ByteWebConnector.SDK.CorrelationHandlersAndMiddleware;
using Serilog.Context;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ByteWebConnector.SDK
{
    public class Startup
    {
        private static HttpClient httpClient = new HttpClient();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var elmahResponse = AsyncHelper.RunSync(func: () => httpClient.GetAsync(requestUri: $"{Configuration[key: "KeyStore:Url"]}/api/keystore/keystore?key=ElmahCS"));
            elmahResponse.EnsureSuccessStatusCode();
            var elmahKey = AsyncHelper.RunSync(func: () => elmahResponse.Content.ReadAsStringAsync());
            services.AddElmah<SqlErrorLog>(options =>
            {
                options.ConnectionString = elmahKey;
            });

            services.AddScoped<IMismoConverter, MismoConverter34>();
            services.AddScoped<ITextFileWriter, TextFileWriter>();
            services.AddScoped<IByteSDKHelper, ByteSDKHelper>();
            services.AddHttpContextAccessor();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use(async (httpContext, next) =>
            {
                var correlationId = httpContext.Request.Headers["CorrelationId"];
                if (correlationId.Count > 0)
                {
                    var logger = httpContext.RequestServices.GetRequiredService<ILogger<Startup>>();
                    using (logger.BeginScope(new Dictionary<string, object> { ["CorrelationId"] = correlationId[0] }))
                    {
                        await next();
                    }
                }
                else
                    await next();
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware<ExceptionMiddleware>();
            }
            app.UseElmah();
            app.UseMvc();
        }
    }
}

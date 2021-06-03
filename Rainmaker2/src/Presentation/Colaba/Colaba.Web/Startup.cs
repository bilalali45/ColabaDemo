using ElmahCore.Mvc;
using ElmahCore.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Colaba.Web.Helpers;
using Colaba.Web.CorrelationHandlersAndMiddleware;
using System.Security.Authentication;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Rewrite;
using TenantConfig.Common.DistributedCache;
using StackExchange.Redis;

namespace Colaba.Web
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

            #region HttpClient Dependency with correlation

            services.AddTransient<RequestHandler>();
            services.AddHttpClient("clientWithCorrelationId")
                    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
                    {
                        SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13,
                        MaxConnectionsPerServer = int.MaxValue,
                        ServerCertificateCustomValidationCallback = (a,b,c,d)=>true
                    })
                    .AddHttpMessageHandler<RequestHandler>(); //Override SendAsync method 
            services.AddSingleton(implementationFactory: s => s.GetRequiredService<IHttpClientFactory>().CreateClient(name: "clientWithCorrelationId"));
            services.AddHttpContextAccessor();  //For http request context accessing
            services.AddTransient<ICorrelationIdAccessor, CorrelationIdAccessor>();
            #endregion

            services.AddOptions();
            services.AddStackExchangeRedisCache(config=> {
                config.ConfigurationOptions = ConfigurationOptions.Parse(Configuration["Redis:ConnectionString"]);
            });
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimit"));
            services.AddSingleton<IIpPolicyStore, DistributedCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore,DistributedCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration,RateLimitConfiguration>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<LogHeaderMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware<ExceptionMiddleware>();
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseElmah();
            app.UseIpRateLimiting();
            app.Use(async (context, next) =>
            {
                context.Response.OnStarting((state) =>
                {
                    if (!context.WebSockets.IsWebSocketRequest)
                    {
                        context.Response.Headers.Add("Referrer-Policy", new StringValues("no-referrer"));
                        context.Response.Headers.Add("X-Content-Type-Options", new StringValues("nosniff"));
                        context.Response.Headers.Remove("Server");
                    }
                    return Task.CompletedTask;
                }, null);
                await next();
            });

            app.Map("/"+Constants.CDN_PATH, cdnApp => {
                var fileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), Constants.CDN_PATH));
                cdnApp.UseStaticFiles(new StaticFileOptions()
                {
                    ContentTypeProvider = new FileExtensionContentTypeProvider(),
                    FileProvider = fileProvider,
                    OnPrepareResponse = DisableCache
                });
            });

            var options = new RewriteOptions();
            options.Rules.Add(new ColabaRewriteRule());
            app.UseRewriter(options);

            app.Map("/"+Constants.SPA_PATH, spaApp => {
                var fileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), Constants.SPA_PATH));
                spaApp.UseStaticFiles(new StaticFileOptions()
                {
                    ContentTypeProvider = new FileExtensionContentTypeProvider(),
                    FileProvider = fileProvider,
                    OnPrepareResponse = DisableCache
                });
                spaApp.UseSpa(spa =>
                {
                    spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions()
                    {
                        FileProvider = fileProvider,
                        OnPrepareResponse = DisableCache
                    };
                });
                spaApp.Run(async (context) =>
                {
                    context.Response.StatusCode = 404;
                    await context.Response.CompleteAsync();
                });
            });
            
            app.Run(async (context) =>
            {
                context.Response.StatusCode = 404;
                await context.Response.CompleteAsync();
            });
        }

        private void DisableCache(StaticFileResponseContext context)
        {
            // Disable caching of all static files.
            context.Context.Response.Headers["Cache-Control"] = "no-cache, no-store, max-age=0, must-revalidate";
            context.Context.Response.Headers["Pragma"] = "no-cache";
            context.Context.Response.Headers["Expires"] = "-1";
        }
    }
}

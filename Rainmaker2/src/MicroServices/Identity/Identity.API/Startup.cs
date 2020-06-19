using System.Net.Http;
using Identity.CorrelationHandlersAndMiddleware;
using Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Denpendency Injection

            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IKeyStoreService, KeyStoreService>();

            services.AddControllers().AddNewtonsoftJson();
            ;
            //services.AddIdentityServer(x =>
            //        {
            //            x.IssuerUri = "none";
            //        })
            //        .AddDeveloperSigningCredential()
            //        .AddInMemoryApiResources(Config.GetAllApiResources())
            //        .AddInMemoryClients(Config.GetClients(Configuration));

            #region HttpClientDependencies

            services.AddTransient<RequestHandler>();
            services.AddHttpClient(name: "clientWithCorrelationId")
                    .AddHttpMessageHandler<RequestHandler>(); //Override SendAsync method 
            services.AddTransient(implementationFactory: s => s.GetRequiredService<IHttpClientFactory>().CreateClient(name: "clientWithCorrelationId"));
            services.AddHttpContextAccessor(); //For http request context accessing
            services.AddTransient<ICorrelationIdAccessor, CorrelationIdAccessor>();

            #endregion

            #endregion
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env)
        {
            app.UseMiddleware<LogHeaderMiddleware>();
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            //app.UseIdentityServer();
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(configure: endpoints =>
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                endpoints.MapControllers();
            });
        }
    }
}
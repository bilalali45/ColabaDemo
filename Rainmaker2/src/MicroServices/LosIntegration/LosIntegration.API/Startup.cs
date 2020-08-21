using System;
using System.Net.Http;
using System.Security.Authentication;
using LosIntegration.API.CorrelationHandlersAndMiddleware;
using LosIntegration.API.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LosIntegration.Service;
using LosIntegration.Service.Interface;
using Microsoft.EntityFrameworkCore;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;

namespace LosIntegration.API
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
            services.AddControllers();
            var csResponse = AsyncHelper.RunSync(() => httpClient.GetAsync($"{Configuration["ServiceAddress:KeyStore:Url"]}/api/keystore/keystore?key=RainMakerCS"));
            if (!csResponse.IsSuccessStatusCode)
            {
                throw new Exception("Unable to load key store");
            }
            services.AddDbContext<LosIntegration.Data.Context>(options => options.UseSqlServer(Configuration["LosConnectionString"])); //todo shehroz get from keystore
            services.AddScoped<IRepositoryProvider, RepositoryProvider>(x => new RepositoryProvider(new RepositoryFactories()));
            services.AddScoped<IUnitOfWork<LosIntegration.Data.Context>, UnitOfWork<LosIntegration.Data.Context>>();
            //services.AddScoped<ISettingService, SettingService>();
            //services.AddScoped<IStringResourceService, StringResourceService>();
            services.AddScoped<IMappingService, MappingService>();
            services.AddScoped<IByteDocTypeMappingService, ByteDocTypeMappingService>();
            services.AddScoped<IByteDocCategoryMappingService, ByteDocCategoryMappingService>();
            services.AddScoped<IByteDocStatusMappingService, ByteDocStatusMappingService>();
            #region HttpClientDependencies

            services.AddTransient<RequestHandler>();
            services.AddHttpClient(name: "clientWithCorrelationId")
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
                {
                    SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13,
                    MaxConnectionsPerServer = int.MaxValue
                })
                .AddHttpMessageHandler<RequestHandler>(); //Override SendAsync method 
            services.AddTransient(implementationFactory: s => s.GetRequiredService<IHttpClientFactory>().CreateClient(name: "clientWithCorrelationId"));
            services.AddHttpContextAccessor(); //For http request context accessing
            services.AddTransient<ICorrelationIdAccessor, CorrelationIdAccessor>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

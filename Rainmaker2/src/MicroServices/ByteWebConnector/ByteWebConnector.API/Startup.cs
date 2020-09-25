using ByteWebConnector.API.CorrelationHandlersAndMiddleware;
using ByteWebConnector.Data;
using ByteWebConnector.Service.DbServices;
using LosIntegration.API.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;

namespace ByteWebConnector.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private static HttpClient httpClient = new HttpClient();
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var csResponse = AsyncHelper.RunSync(() => httpClient.GetAsync($"{Configuration["ServiceAddress:KeyStore:Url"]}/api/keystore/keystore?key=RainMakerCS"));
            csResponse.EnsureSuccessStatusCode();
            services.AddControllers();

            #region BWC Context

            services.AddDbContext<BwcContext>(options => options.UseSqlServer(Configuration["BWCConnString"]));
            services.AddScoped<IRepositoryProvider, RepositoryProvider>(x => new RepositoryProvider(new RepositoryFactories()));
            services.AddScoped<IUnitOfWork<BwcContext>, UnitOfWork<BwcContext>>();

            #endregion

            
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ByteWebConnector.Service.InternalServices.IRainmakerService, ByteWebConnector.Service.InternalServices.RainmakerService>();
            services.AddScoped<ByteWebConnector.Service.InternalServices.IByteWebConnectorSdkService, ByteWebConnector.Service.InternalServices.ByteWebConnectorSdkService>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            #region HttpClientDependencies

            services.AddTransient<RequestHandler>();
            services.AddHttpClient(name: "clientWithCorrelationId")
                    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
                                                              {
                                                                  SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13,
                                                                  MaxConnectionsPerServer = int.MaxValue,
                                                                  ServerCertificateCustomValidationCallback = (a,b,c,d)=>true
                                                              })
                    .AddHttpMessageHandler<RequestHandler>(); //Override SendAsync method 
            services.AddSingleton(implementationFactory: s => s.GetRequiredService<IHttpClientFactory>().CreateClient(name: "clientWithCorrelationId"));
            services.AddHttpContextAccessor(); //For http request context accessing
            services.AddTransient<ICorrelationIdAccessor, CorrelationIdAccessor>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<LogHeaderMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

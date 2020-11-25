using System;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using AutoMapper;
using ByteWebConnector.API.CorrelationHandlersAndMiddleware;
using ByteWebConnector.Data;
using ByteWebConnector.Service.DbServices;
using ByteWebConnector.Service.ExternalServices;
using ByteWebConnector.Service.InternalServices;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using LosIntegration.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;
using AppContext = ServiceCallHelper.AppContext;

namespace ByteWebConnector.API
{
    public class Startup
    {
        private static readonly HttpClient httpClient = new HttpClient();


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container. 

        public void ConfigureServices(IServiceCollection services)
        {
            var elmahResponse = AsyncHelper.RunSync(func: () => httpClient.GetAsync(requestUri: $"{Configuration[key: "ServiceAddress:KeyStore:Url"]}/api/keystore/keystore?key=ElmahCS"));
            elmahResponse.EnsureSuccessStatusCode();
            var elmahKey = AsyncHelper.RunSync(func: () => elmahResponse.Content.ReadAsStringAsync());
            services.AddElmah<SqlErrorLog>(options =>
            {
                options.ConnectionString = elmahKey;
            });
            var csResponse = AsyncHelper.RunSync(func: () => httpClient.GetAsync(requestUri: $"{Configuration[key: "ServiceAddress:KeyStore:Url"]}/api/keystore/keystore?key=RainMakerCS"));
            csResponse.EnsureSuccessStatusCode();
            services.AddControllers();

            services.AddAutoMapper(typeof(Startup));

            #region BWC Context

            services.AddDbContext<BwcContext>(optionsAction: options => options.UseSqlServer(connectionString: Configuration[key: "BWCConnString"]));
            services.AddScoped<IRepositoryProvider, RepositoryProvider>(implementationFactory: x => new RepositoryProvider(repositoryFactories: new RepositoryFactories()));
            services.AddScoped<IUnitOfWork<BwcContext>, UnitOfWork<BwcContext>>();

            #endregion

            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IRainmakerService, RainmakerService>();
            services.AddScoped<IByteProService, ByteProService>();
            services.AddScoped<ILosIntegrationService, LosIntegrationService>();
            services.AddScoped<IByteWebConnectorSdkService, ByteWebConnectorSdkService>();
            Encoding.RegisterProvider(provider: CodePagesEncodingProvider.Instance);

            #region HttpClientDependencies

            services.AddTransient<RequestHandler>();
            services.AddHttpClient(name: "clientWithCorrelationId")
                    .ConfigurePrimaryHttpMessageHandler(configureHandler: () =>
                    {
                        var httpClientHandler = new HttpClientHandler
                        {
                            SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13,
                            MaxConnectionsPerServer = int.MaxValue,
                            ServerCertificateCustomValidationCallback = (a,
                                                                         b,
                                                                         c,
                                                                         d) => true,
                        };

                        //httpClientHandler.Proxy = new WebProxy(Address: "127.0.0.1:8888");
                        return httpClientHandler;
                    })
                    .AddHttpMessageHandler<RequestHandler>(); //Override SendAsync method 
            services.AddSingleton(implementationFactory: s => s.GetRequiredService<IHttpClientFactory>().CreateClient(name: "clientWithCorrelationId"));
            services.AddHttpContextAccessor(); //For http request context accessing
            services.AddTransient<ICorrelationIdAccessor, CorrelationIdAccessor>();

            #endregion

            #region Authentication Setup

            var keyResponse = AsyncHelper.RunSync(func: () => httpClient.GetAsync(requestUri: $"{Configuration[key: "ServiceAddress:KeyStore:Url"]}/api/keystore/keystore?key=JWT"));
            keyResponse.EnsureSuccessStatusCode();
            var securityKey = AsyncHelper.RunSync(func: () => keyResponse.Content.ReadAsStringAsync());
            var symmetricSecurityKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: securityKey));
            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(configureOptions: options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            //what to validate
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateIssuerSigningKey = true,
                            //setup validate data
                            ValidIssuer = "rainsoftfn",
                            ValidAudience = "readers",
                            IssuerSigningKey = symmetricSecurityKey
                        };
                    });

            #endregion
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env)
        {
            app.UseMiddleware<LogHeaderMiddleware>();
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseElmah();
            app.UseRouting();

            AppContext.Configure(httpContextAccessor:
                                 app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(configure: endpoints => { endpoints.MapControllers(); });
        }
    }
}
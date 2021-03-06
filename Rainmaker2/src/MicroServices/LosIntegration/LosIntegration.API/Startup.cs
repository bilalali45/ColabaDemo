using LosIntegration.API.CorrelationHandlersAndMiddleware;
using LosIntegration.API.Helpers;
using LosIntegration.Service;
using LosIntegration.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using LosIntegration.Service.InternalServices;
using LosIntegration.Service.InternalServices.Rainmaker;
using Microsoft.AspNetCore.Http;
using ServiceCallHelper;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
            var elmahResponse = AsyncHelper.RunSync(func: () => httpClient.GetAsync(requestUri: $"{Configuration[key: "ServiceAddress:KeyStore:Url"]}/api/keystore/keystore?key=ElmahCS"));
            elmahResponse.EnsureSuccessStatusCode();
            var elmahKey = AsyncHelper.RunSync(func: () => elmahResponse.Content.ReadAsStringAsync());
            services.AddElmah<SqlErrorLog>(options =>
            {
                options.ConnectionString = elmahKey;
            });
            services.AddControllers();
            var csResponse = AsyncHelper.RunSync(() => httpClient.GetAsync($"{Configuration["ServiceAddress:KeyStore:Url"]}/api/keystore/keystore?key=RainMakerCS"));
            csResponse.EnsureSuccessStatusCode();

            var losCsResponse = AsyncHelper.RunSync(func: () => httpClient.GetAsync(requestUri: $"{Configuration[key: "ServiceAddress:KeyStore:Url"]}/api/keystore/keystore?key=LosCS"));
            losCsResponse.EnsureSuccessStatusCode();
            services.AddDbContext<LosIntegration.Data.LosIntegrationContext>(optionsAction: options => options.UseSqlServer(connectionString: AsyncHelper.RunSync(() => losCsResponse.Content.ReadAsStringAsync())));

            //services.AddDbContext<LosIntegration.Data.LosIntegrationContext>(options => options.UseSqlServer(Configuration["LosConnectionString"])); //todo shehroz get from keystore
            services.AddScoped<IRepositoryProvider, RepositoryProvider>(x => new RepositoryProvider(new RepositoryFactories()));
            services.AddScoped<IUnitOfWork<LosIntegration.Data.LosIntegrationContext>, UnitOfWork<LosIntegration.Data.LosIntegrationContext>>();
            services.AddScoped<IMappingService, MappingService>();
            services.AddScoped<IByteDocTypeMappingService, ByteDocTypeMappingService>();
            services.AddScoped<IByteDocCategoryMappingService, ByteDocCategoryMappingService>();
            services.AddScoped<IByteDocStatusMappingService, ByteDocStatusMappingService>();
            services.AddScoped<IRainmakerService, RainmakerService>();
            services.AddScoped<ILoanApplicationService, LoanApplicationService>();
            services.AddScoped<IByteWebConnectorService, ByteWebConnectorService>();
            services.AddScoped<ILoanRequestService, LoanRequestService>();
            services.AddScoped<IThirdPartyCodeService, ThirdPartyCodeService>();
            services.AddScoped<IMilestoneService, MilestoneService>();

            #region HttpClientDependencies

            services.AddTransient<RequestHandler>();
            services.AddHttpClient(name: "clientWithCorrelationId")
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
                {
                    SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13,
                    MaxConnectionsPerServer = int.MaxValue
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<LogHeaderMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseElmah();
            app.UseRouting();

            AppContext.Configure(httpContextAccessor:
                                 app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

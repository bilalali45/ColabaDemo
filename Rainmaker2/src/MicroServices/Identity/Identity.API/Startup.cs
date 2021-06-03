using ElmahCore.Mvc;
using ElmahCore.Sql;
using Identity.CorrelationHandlersAndMiddleware;
using Identity.Data;
using Identity.Helpers;
using Identity.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using System;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using Identity.Service.Helpers;
using Identity.Service.Helpers.Interfaces;
using TenantConfig.Common;
using Identity.Service.Mobile;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TenantConfig.Data;
using TokenCacheHelper.TokenManager;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;

namespace Identity
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

            var csResponse = AsyncHelper.RunSync(() => httpClient.GetAsync($"{Configuration["KeyStore:Url"]}/api/keystore/keystore?key=TenantConfigCS"));
            csResponse.EnsureSuccessStatusCode();
            services.AddDbContext<TenantConfigContext>(options => options.UseSqlServer(AsyncHelper.RunSync(() => csResponse.Content.ReadAsStringAsync())));
            services.AddScoped<IUnitOfWork<TenantConfigContext>>(factory => new UnitOfWork<TenantConfigContext>(factory.GetRequiredService<TenantConfigContext>(), new RepositoryProvider(new RepositoryFactories()),factory.GetRequiredService<IHttpContextAccessor>()));

            var csResponse1 = AsyncHelper.RunSync(() => httpClient.GetAsync($"{Configuration["KeyStore:Url"]}/api/keystore/keystore?key=IdentityCS"));
            csResponse1.EnsureSuccessStatusCode();
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(AsyncHelper.RunSync(() => csResponse1.Content.ReadAsStringAsync())));
            services.AddScoped<IUnitOfWork<IdentityContext>>(factory => new UnitOfWork<IdentityContext>(factory.GetRequiredService<IdentityContext>(), new RepositoryProvider(new RepositoryFactories()), factory.GetRequiredService<IHttpContextAccessor>()));

            var keyResponse = AsyncHelper.RunSync(func: () => httpClient.GetAsync(requestUri: $"{Configuration[key: "KeyStore:Url"]}/api/keystore/keystore?key=JWT"));
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

            #region Denpendency Injection

            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<Service.IKeyStoreService, Service.KeyStoreService>();
            services.AddScoped<ICustomerAccountService, CustomerAccountService>();
            services.AddScoped<ITenantConfigService, TenantConfigService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ValidateRecaptchaAttribute>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IOtpTracingService, OtpTracingService>();
            services.AddScoped<ITwoFaHelper, TwoFaHelper>();
            services.AddScoped<IMcuAccountService, McuAccountService>();
            services.AddScoped<ITwoFaHelperV2, TwoFaHelperV2>();
            

            services.AddTransient<ITokenManager, TokenManager>();

            services.AddControllers().AddNewtonsoftJson();
            

            #region HttpClientDependencies

            services.AddTransient<RequestHandler>();
            services.AddHttpClient(name: "clientWithCorrelationId")
                    .ConfigurePrimaryHttpMessageHandler(()=>new HttpClientHandler()
                    {
                        SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13,
                        MaxConnectionsPerServer = int.MaxValue
                    })
                    .AddHttpMessageHandler<RequestHandler>(); //Override SendAsync method 
            services.AddSingleton(implementationFactory: s => s.GetRequiredService<IHttpClientFactory>().CreateClient(name: "clientWithCorrelationId"));
            services.AddHttpContextAccessor(); //For http request context accessing
            services.AddTransient<ICorrelationIdAccessor, CorrelationIdAccessor>();
            services.AddTransient<ITwoFactorAuth, TwilioTwoFactorAuthService>();
            services.AddTransient<IRestClient, RestClient>();

            #endregion

            #endregion

            var redisParams = AsyncHelper.RunSync(func: () => httpClient.GetAsync(requestUri: $"{Configuration[key: "KeyStore:Url"]}/api/keystore/keystore?key=RedisIdentityConfig"));
            var redisConfig = AsyncHelper.RunSync(() => redisParams.Content.ReadAsStringAsync());
            var redisIdentityConfig = JsonConvert.DeserializeObject<RedisConfiguration>(redisConfig);
            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisIdentityConfig);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env, IServiceProvider services)
        {
            app.UseMiddleware<LogHeaderMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware<ExceptionMiddleware>();
            }
            app.UseElmah();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(configure: endpoints =>
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                endpoints.MapControllers();
            });
            
        }
    }
}
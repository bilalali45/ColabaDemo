using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Milestone.API.CorrelationHandlersAndMiddleware;
using Milestone.API.Helpers;
using Milestone.Data;
using Milestone.Service;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;

namespace Milestone.API
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
            var csResponse = AsyncHelper.RunSync(() => httpClient.GetAsync($"{Configuration["KeyStore:Url"]}/api/keystore/keystore?key=MilestoneCS"));
            csResponse.EnsureSuccessStatusCode();
            services.AddDbContext<MilestoneContext>(options => options.UseSqlServer(AsyncHelper.RunSync(() => csResponse.Content.ReadAsStringAsync())));
            services.AddScoped<IRepositoryProvider, RepositoryProvider>(x => new RepositoryProvider(new RepositoryFactories()));
            services.AddScoped<IUnitOfWork<MilestoneContext>, UnitOfWork<MilestoneContext>>();
            services.AddScoped<IMilestoneService, MilestoneService>();
            services.AddScoped<IRainmakerService, RainmakerService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddControllers();
            var keyResponse = AsyncHelper.RunSync(() => httpClient.GetAsync($"{Configuration["KeyStore:Url"]}/api/keystore/keystore?key=JWT"));
            csResponse.EnsureSuccessStatusCode();
            var securityKey = AsyncHelper.RunSync(() => keyResponse.Content.ReadAsStringAsync());
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

            #region HttpClient Dependency with correlation

            services.AddTransient<RequestHandler>();
            services.AddHttpClient("clientWithCorrelationId")
                    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
                    {
                        SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13,
                        MaxConnectionsPerServer = int.MaxValue
                    })
                    .AddHttpMessageHandler<RequestHandler>(); //Override SendAsync method 
            services.AddSingleton(implementationFactory: s => s.GetRequiredService<IHttpClientFactory>().CreateClient(name: "clientWithCorrelationId"));
            services.AddHttpContextAccessor();  //For http request context accessing
            services.AddTransient<ICorrelationIdAccessor, CorrelationIdAccessor>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

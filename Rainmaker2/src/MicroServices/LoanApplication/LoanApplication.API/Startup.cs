using ElmahCore.Mvc;
using ElmahCore.Sql;
using LoanApplication.API.CorrelationHandlersAndMiddleware;
using LoanApplication.API.Helpers;
using LoanApplication.Service;
using LoanApplicationDb.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Data;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;

namespace LoanApplication.API
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
            services.AddScoped<IUnitOfWork<TenantConfigContext>>(factory => new UnitOfWork<TenantConfigContext>(factory.GetRequiredService<TenantConfigContext>(), new RepositoryProvider(new RepositoryFactories()), factory.GetRequiredService<IHttpContextAccessor>()));

            var csResponse1 = AsyncHelper.RunSync(() => httpClient.GetAsync($"{Configuration["KeyStore:Url"]}/api/keystore/keystore?key=LoanApplicationCS"));
            csResponse1.EnsureSuccessStatusCode();
            services.AddDbContext<LoanApplicationContext>(options => options.UseSqlServer(AsyncHelper.RunSync(() => csResponse1.Content.ReadAsStringAsync())));
            services.AddScoped<IUnitOfWork<LoanApplicationContext>>(factory => new UnitOfWork<LoanApplicationContext>(factory.GetRequiredService<LoanApplicationContext>(), new RepositoryProvider(new RepositoryFactories()), factory.GetRequiredService<IHttpContextAccessor>()));

            services.AddControllers();
            services.AddScoped<IInfoService, InfoService>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<ILoanGoalService, LoanGoalService>();
            //services.AddScoped<IPrimaryBorrowerService, PrimaryBorrowerService>();
            //services.AddScoped<ISecondaryBorrowerService, SecondaryBorrowerService>();
            services.AddScoped<ILoanContactService, LoanContactService>();
            services.AddScoped<IBorrowerConsentService, BorrowerConsentService>();
            services.AddScoped<IVaDetailService, VaDetailService>();
            services.AddScoped<IBorrowerService, BorrowerService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<ISubjectPropertyService, SubjectPropertyService>();
            services.AddScoped<IDbFunctionService, DbFunctionService>();
            services.AddScoped<IIncomeService, IncomeService>();
            services.AddScoped<IEmploymentService, EmploymentService>();
            services.AddScoped<IAssetsService, AssetsService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IMyPropertyService, MyPropertyService>();
            services.AddScoped<IFinishingUpService, FinishingUpService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IRefinancePropertyService, RefinancePropertyService>();

            

            var keyResponse = AsyncHelper.RunSync(() => httpClient.GetAsync($"{Configuration["KeyStore:Url"]}/api/keystore/keystore?key=JWT"));
            keyResponse.EnsureSuccessStatusCode();
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

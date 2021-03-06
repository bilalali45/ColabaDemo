using ElmahCore.Mvc;
using ElmahCore.Sql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Rainmaker.API.CorrelationHandlersAndMiddleware;
using Rainmaker.API.Helpers;
using Rainmaker.Service;
using Rainmaker.Service.Helpers;
using RainMaker.API.CorrelationHandlersAndMiddleware;
using RainMaker.Service;
using RainMaker.Service.Helpers;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;

namespace Rainmaker.API
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
            var csResponse = AsyncHelper.RunSync(() => httpClient.GetAsync($"{Configuration["KeyStore:Url"]}/api/keystore/keystore?key=RainMakerCS"));
            csResponse.EnsureSuccessStatusCode();
            services.AddDbContext<RainMaker.Data.RainMakerContext>(options => options.UseSqlServer(AsyncHelper.RunSync(()=> csResponse.Content.ReadAsStringAsync())));
            services.AddScoped<IRepositoryProvider, RepositoryProvider>(x => new RepositoryProvider(new RepositoryFactories()));
            services.AddScoped<IUnitOfWork<RainMaker.Data.RainMakerContext>, UnitOfWork<RainMaker.Data.RainMakerContext>>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IStringResourceService, StringResourceService>();
            services.AddSingleton<ICommonService, CommonService>();
            services.AddScoped<ILoanApplicationService, LoanApplicationService>();
            services.AddScoped<ILoanRequestService, LoanRequestService>();
            services.AddScoped<IThirdPartyCodeService, ThirdPartyCodeService>();
            services.AddScoped<IOpportunityService, OpportunityService>();
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<IFtpHelper,FtpHelper>();
            services.AddScoped<ISitemapService, SitemapService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IWorkQueueService, WorkQueueService>();
            services.AddScoped<IBorrowerService, BorrowerService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
         
            services.AddControllers().AddNewtonsoftJson(options =>
                                                           options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                                      );
            
            var keyResponse= AsyncHelper.RunSync(()=> httpClient.GetAsync($"{Configuration["KeyStore:Url"]}/api/keystore/keystore?key=JWT"));
            keyResponse.EnsureSuccessStatusCode();
            var securityKey = AsyncHelper.RunSync(()=>keyResponse.Content.ReadAsStringAsync());
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

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
using Setting.API.CorrelationHandlersAndMiddleware;
using Setting.API.Helpers;
using Setting.Data;
using Setting.Service;
using System;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Logging;

namespace Setting.API
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
            var csResponse = AsyncHelper.RunSync(() => httpClient.GetAsync($"{Configuration["KeyStore:Url"]}/api/keystore/keystore?key=SettingCS"));
            csResponse.EnsureSuccessStatusCode();
            services.AddDbContext<SettingContext>(options => options.UseSqlServer(AsyncHelper.RunSync(() => csResponse.Content.ReadAsStringAsync())));
            services.AddScoped<IRepositoryProvider, RepositoryProvider>(x => new RepositoryProvider(new RepositoryFactories()));
            services.AddScoped<IUnitOfWork<SettingContext>, UnitOfWork<SettingContext>>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IRainmakerService, RainmakerService>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();
            services.AddScoped<IEmailReminderService, EmailReminderService>();
            services.AddScoped<IBackgroundService, HangfireBackgroundService>(x=>new HangfireBackgroundService(x.GetRequiredService<HttpClient>(),
                                                                                                                    x.GetRequiredService<IEmailReminderService>(),
                                                                                                                    x.GetRequiredService<IConfiguration>(),
                                                                                                                    x.GetRequiredService<IEmailTemplateService>(),
                                                                                                                    x.GetRequiredService<IRainmakerService>(),x.GetRequiredService<ILogger<HangfireBackgroundService>>()));
            services.AddControllers().AddNewtonsoftJson();
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
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                var accessToken = context.Request.Query["access_token"];

                                // If the request is for our hub...
                                var path = context.HttpContext.Request.Path;
                                if (!string.IsNullOrEmpty(accessToken) &&
                                    (path.StartsWithSegments("/serverhub")))
                                {
                                    // Read the token out of the query string
                                    context.Token = accessToken;
                                }
                                return Task.CompletedTask;
                            }
                        };
                    });

            // Add Hangfire services.
            var csHangfireResponse = AsyncHelper.RunSync(() => httpClient.GetAsync($"{Configuration["KeyStore:Url"]}/api/keystore/keystore?key=HangfireCS"));
            csHangfireResponse.EnsureSuccessStatusCode();
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(AsyncHelper.RunSync(() => csHangfireResponse.Content.ReadAsStringAsync()), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services, IBackgroundJobClient backgroundJobs)
        {
            // Register Background jobs
            var hangFireService = services.GetRequiredService<IBackgroundService>();
            hangFireService.RegisterJob();

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
                endpoints.MapHangfireDashboard();
            });
        }
    }
}

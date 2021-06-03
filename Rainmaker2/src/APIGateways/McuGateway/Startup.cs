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
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.RateLimit;
using RainsoftGateway.Core.Helpers;
using RainsoftGateway.Core.Middleware;
using RainsoftGateway.Core.Services;
using StackExchange.Redis;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Data;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;

namespace McuGateway
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
            services.AddHttpContextAccessor();
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
            services.AddScoped<IRepositoryProvider, RepositoryProvider>(x => new RepositoryProvider(new RepositoryFactories()));
            services.AddScoped<IUnitOfWork<TenantConfigContext>, UnitOfWork<TenantConfigContext>>();
            services.AddScoped<ITenantResolver, TenantResolver>();
            services.AddHttpClient();

            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                                         builder =>
                                         {
                                             builder.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                                             //builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                         });
            });

            #region JWT

            /**START : JWT**************************************************************/

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
            /**END : JWT**************************************************************/

            #endregion
            services.AddTransient<IConnectionMultiplexer>((services) => { return ConnectionMultiplexer.Connect(ConfigurationOptions.Parse(Configuration["Redis:ConnectionString"])); });
            services.AddStackExchangeRedisCache(config => {
                config.ConfigurationOptions = ConfigurationOptions.Parse(Configuration["Redis:ConnectionString"]);
            });
            services.AddSingleton<IRateLimitCounterHandler, Services.DistributedCacheRateLimitCounterHandler>();
            services.AddOcelot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<LogHeaderMiddleware>();
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware<ExceptionMiddleware>();
                //    app.UseHsts();
            }
            app.UseElmah();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Use(async (context, next) =>
            {
                await context.RequestServices.GetRequiredService<ITenantResolver>().ResolveTenant(context);
                await next();
            });
            app.UseEndpoints(endpoints =>
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                endpoints.MapControllers();
            });
            app.Use(async (context, next) =>
            {
                context.Response.OnStarting((state) =>
                {
                    if (!context.WebSockets.IsWebSocketRequest)
                    {
                        //context.Response.Headers.Add("Referrer-Policy", new StringValues("no-referrer"));
                        //context.Response.Headers.Add("X-Content-Type-Options", new StringValues("nosniff"));
                        //if (context.Response.Headers["Content-Disposition"].Count <= 0)
                        //    context.Response.Headers.Add("Content-Disposition", "attachment; filename=\"api.json\"");
                        context.Response.Headers.Remove("Server");
                    }
                    return Task.CompletedTask;
                }, null);
                await next();
            });
            app.UseWebSockets();
            app.UseOcelot().Wait();
        }
    }
}

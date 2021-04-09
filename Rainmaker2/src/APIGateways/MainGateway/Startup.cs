using MainGateway.Helpers;
using MainGateway.Middleware;
using MainGateway.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System;
using Newtonsoft.Json;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using TokenCacheHelper.CacheHandler;
using TokenCacheHelper.TokenManager;
using Microsoft.Extensions.Logging;

namespace MainGateway
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


            services.Configure<AllowedApisOptions>(Configuration.GetSection(
                                        AllowedApisOptions.AllowedApis));
            services.AddHttpClient();

            services.AddTransient<IKeyStoreService, KeyStoreService>();
            
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                                         builder =>
                                         {
                                             builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                         });
            });

            #region JWT

            /**START : JWT**************************************************************/

            var keyStoreService = services.BuildServiceProvider().GetRequiredService<IKeyStoreService>();

            var securityKey =   keyStoreService.GetJwtSecurityKey();
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

            services.AddOcelot();

            services.AddTransient<ITokenManager, TokenManager>();

            var redisParams = AsyncHelper.RunSync(func: () => httpClient.GetAsync(requestUri: $"{Configuration[key: "KeyStore:Url"]}/api/keystore/keystore?key=RedisIdentityConfig"));
            var redisConfig = AsyncHelper.RunSync(() => redisParams.Content.ReadAsStringAsync());
            var redisIdentityConfig = JsonConvert.DeserializeObject<RedisConfiguration>(redisConfig);


            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisIdentityConfig);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context,next) => {
                IOptions<AllowedApisOptions> apis = context.RequestServices.GetRequiredService<IOptions<AllowedApisOptions>>();
                ILogger<Startup> logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();
                if(!context.WebSockets.IsWebSocketRequest)
                {
                    if(!apis.Value.Apis.Contains(context.Request.Path.ToString(),StringComparer.OrdinalIgnoreCase))
                    {
                        logger.LogError("Unable to find request path in white list");
                        context.Response.StatusCode = 404;
                        await context.Response.CompleteAsync();
                    }
                    else
                    {
                        await next();
                    }
                }
                else
                {
                    await next();
                }
            });
            app.UseMiddleware<LogHeaderMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            
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
                        context.Response.Headers.Add("Referrer-Policy", new StringValues("no-referrer"));
                        context.Response.Headers.Add("X-Content-Type-Options", new StringValues("nosniff"));
                        if (context.Response.Headers["Content-Disposition"].Count <= 0)
                            context.Response.Headers.Add("Content-Disposition", "attachment; filename=\"api.json\"");
                        context.Response.Headers.Remove("Server");
                    }
                    return Task.CompletedTask;
                },null);
                await next();
            });
            app.UseWebSockets();
            app.UseOcelot().Wait();
        }
    }
}

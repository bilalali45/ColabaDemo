using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using IdentityServer4.AccessTokenValidation;
using MainGateway.Middleware;
using MainGateway.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace MainGateway
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



            services.AddHttpClient();

            services.AddTransient<IKeyStoreService, KeyStoreService>();
            
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                                         builder =>
                                         {
                                             var itemArray = Configuration.GetSection("AllowedOrigins").GetChildren().Select(c => c.Value).ToArray();
                                             //builder.WithOrigins(itemArray).AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("Content-Disposition", "Content-Length");
                                             builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                         });
            });



            #region IdentityServer4 Authentication

            //var authenticationProviderKey = "TestKey";
            //Action<IdentityServerAuthenticationOptions> opt = o =>
            //{
            //    o.Authority = "http://localhost:5010";
            //    o.ApiName = "SampleService";
            //    o.SupportedTokens = SupportedTokens.Both;
            //    o.RequireHttpsMetadata = false;
            //};

            //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //        .AddIdentityServerAuthentication(authenticationProviderKey, opt);

            #endregion


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
                app.UseHsts();
            }

            app.UseMiddleware<RequestResponseLoggingMiddleware>();
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
                await next();
                try
                {
                    if (!context.WebSockets.IsWebSocketRequest)
                    {
                        context.Response.Headers.Add("Referrer-Policy", new StringValues("no-referrer"));
                        context.Response.Headers.Add("X-Content-Type-Options", new StringValues("nosniff"));
                        if (context.Response.Headers["Content-Disposition"].Count <= 0)
                            context.Response.Headers.Add("Content-Disposition", "attachment; filename=\"api.json\"");
                        context.Response.Headers.Remove("Server");
                    }
                }
                catch
                {
                }
            });
            app.UseWebSockets();
            app.UseOcelot().Wait();
        }
    }
}

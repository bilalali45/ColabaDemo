﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Identity.CorrelationHandlersAndMiddleware;
using Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            #region Denpendency Injection
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IKeyStoreService,KeyStoreService >();


            services.AddControllers().AddNewtonsoftJson(); ;
            //services.AddIdentityServer(x =>
            //        {
            //            x.IssuerUri = "none";
            //        })
            //        .AddDeveloperSigningCredential()
            //        .AddInMemoryApiResources(Config.GetAllApiResources())
            //        .AddInMemoryClients(Config.GetClients(Configuration));
            services.AddTransient<RequestHandler>();
            services.AddHttpClient("clientWithCorrelationId")
                    .AddHttpMessageHandler<RequestHandler>(); //Override SendAsync method 
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
            //app.UseIdentityServer();
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                endpoints.MapControllers();
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rainmaker.Service;
using Rainmaker.Service.Helpers;
using RainMaker.Service;
using RainMaker.Service.Helpers;
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
            var csResponse = httpClient.GetAsync($"{Configuration["KeyStore:Url"]}/api/keystore/keystore?key=RainMakerCS").Result;
            if (!csResponse.IsSuccessStatusCode)
            {
                throw new Exception("Unable to load key store");
            }
            services.AddDbContext<RainMaker.Data.RainMakerContext>(options => options.UseSqlServer(csResponse.Content.ReadAsStringAsync().Result));
            services.AddScoped<IRepositoryProvider, RepositoryProvider>(x => new RepositoryProvider(new RepositoryFactories()));
            services.AddScoped<IUnitOfWork<RainMaker.Data.RainMakerContext>, UnitOfWork<RainMaker.Data.RainMakerContext>>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IStringResourceService, StringResourceService>();
            services.AddSingleton<ICommonService, CommonService>();
            services.AddScoped<ILoanApplicationService, LoanApplicationService>();
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<IFtpHelper,FtpHelper>();
            services.AddControllers().AddNewtonsoftJson(options =>
                                                           options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                                      ); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using System;
using System.Net.Http;
using System.Text;
using DocumentManagement.API.CorrelationHandlersAndMiddleware;
using DocumentManagement.API.Helpers;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ExceptionMiddleware = DocumentManagement.API.Helpers.ExceptionMiddleware;

namespace DocumentManagement.API
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
            #region Dependency Injection

            services.AddControllers();
            services.AddScoped<IMongoService, MongoService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFileEncryptionFactory, FileEncryptionFactory>();
            services.AddScoped<IFtpClient, FtpClient>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddHttpClient();
            services.AddScoped<IKeyStoreService, KeyStoreService>();
            services.AddScoped<IAdminDashboardService, AdminDashboardService>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IDocumentService, DocumentService>();
            #region HttpClientDependencies

            services.AddTransient<RequestHandler>();
            services.AddHttpClient(name: "clientWithCorrelationId")
                    .AddHttpMessageHandler<RequestHandler>(); //Override SendAsync method 
            services.AddTransient(implementationFactory: s => s.GetRequiredService<IHttpClientFactory>().CreateClient(name: "clientWithCorrelationId"));
            services.AddHttpContextAccessor(); //For http request context accessing
            services.AddTransient<ICorrelationIdAccessor, CorrelationIdAccessor>();

            #endregion

            #endregion

            var keyResponse = AsyncHelper.RunSync(func: () => httpClient.GetAsync(requestUri: $"{Configuration[key: "KeyStore:Url"]}/api/keystore/keystore?key=JWT"));
            if (!keyResponse.IsSuccessStatusCode) throw new Exception(message: "Unable to load key store");
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
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseMiddleware<ExceptionMiddleware>();

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(configure: endpoints => { endpoints.MapControllers(); });
        }
    }
}
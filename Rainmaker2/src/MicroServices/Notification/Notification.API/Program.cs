using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Notification.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServicePointManager.DefaultConnectionLimit = int.MaxValue;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
            ThreadPool.SetMaxThreads(1000, 1000);
            ThreadPool.SetMinThreads(1000, 1000);
            ConfigureLogging();
            CreateHost(args: args);
        }


        private static void ConfigureLogging()
        {
            var environment = Environment.GetEnvironmentVariable(variable: "ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                                .AddJsonFile(path: "appsettings.json",
                                             optional: false,
                                             reloadOnChange: true)
                                .AddJsonFile(
                                             path: $"appsettings.{Environment.GetEnvironmentVariable(variable: "ASPNETCORE_ENVIRONMENT")}.json",
                                             optional: true)
                                .Build();
            Log.Logger = new LoggerConfiguration()
                         .Enrich.WithCorrelationIdHeader(headerKey: "CorrelationId")
                         .Enrich.FromLogContext()
                         .Enrich.WithExceptionDetails()
                         .Enrich.WithMachineName()
                         //.WriteTo.Debug()
                         //.WriteTo.Console()
                         .WriteTo.Async(x => x.RollingFile("logs\\log-{Date}.log"))
                         .WriteTo.Elasticsearch(options: ConfigureElasticSink(configuration: configuration,
                                                                              environment: environment))
                         .Enrich.WithProperty(name: "Environment",
                                              value: environment)
                         .ReadFrom.Configuration(configuration: configuration)
                         .CreateLogger();
        }


        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration,
                                                                     string environment)
        {
            return new ElasticsearchSinkOptions(node: new Uri(uriString: configuration[key: "ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(oldValue: ".", newValue: "-")}-{environment?.ToLower().Replace(oldValue: ".", newValue: "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
        }


        private static void CreateHost(string[] args)
        {
            try
            {
                CreateHostBuilder(args: args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(messageTemplate: $"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}",
                          propertyValue: ex);
                throw;
            }
        }


        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args: args)
                       .ConfigureWebHostDefaults(configure: webBuilder => { webBuilder.UseStartup<Startup>(); })
                       .ConfigureAppConfiguration(configureDelegate: configuration =>
                       {
                           configuration.AddJsonFile(path: "appsettings.json",
                                                     optional: false,
                                                     reloadOnChange: true);
                           configuration.AddJsonFile(
                                                     path: $"appsettings.{Environment.GetEnvironmentVariable(variable: "ASPNETCORE_ENVIRONMENT")}.json",
                                                     optional: true);
                       })
                       .UseSerilog();
        }
    }
}

using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace LosIntegration.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
                                             path:
                                             $"appsettings.{Environment.GetEnvironmentVariable(variable: "ASPNETCORE_ENVIRONMENT")}.json",
                                             optional: true)
                                .Build();
            Log.Logger = new LoggerConfiguration()
                         .Enrich.WithCorrelationIdHeader(headerKey: "CorrelationId")
                         .Enrich.FromLogContext()
                         .Enrich.WithExceptionDetails()
                         .Enrich.WithMachineName()
                         //.WriteTo.Debug()
                         //.WriteTo.Console()
                         .WriteTo.Async(configure: x => x.File(path: $"Logs\\{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(oldValue: ".", newValue: "-")}-log.log",
                                                               retainedFileCountLimit: 7,
                                                               outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{CorrelationId}] [{Level}] {Message}{NewLine}{Exception}", rollingInterval: RollingInterval.Day)
                                       )
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
            return
                new ElasticsearchSinkOptions(node: new Uri(uriString: configuration[key: "ElasticConfiguration:Uri"]))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat =
                        $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(oldValue: ".", newValue: "-")}-{environment?.ToLower().Replace(oldValue: ".", newValue: "-")}-{DateTime.UtcNow:yyyy-MM}"
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
                                                     path:
                                                     $"appsettings.{Environment.GetEnvironmentVariable(variable: "ASPNETCORE_ENVIRONMENT")}.json",
                                                     optional: true);
                           configuration.AddJsonFile(path: Path.Combine(path1: "Configuration",
                                                                        path2: "serviceDiscovery.json"),
                                                     optional: true,
                                                     reloadOnChange: true);
                       })
                       .UseSerilog();
        }
    }
}
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ByteWebConnector.SDK
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));
#if DEBUG
            isService = false;
#endif
            Environment.CurrentDirectory = AppContext.BaseDirectory;
            ConfigureLogging(args);
            CreateHost(args: args,isService);
        }


        private static void ConfigureLogging(string[] args)
        {
            var environment = string.Empty;
            foreach (var arg in args)
            {
                if (arg.Contains("--environment="))
                {
                    environment = arg.Replace("--environment=", "");
                    break;
                }
            }
            var configuration = new ConfigurationBuilder()
                                .AddJsonFile(path: "appsettings.json",
                                             optional: false,
                                             reloadOnChange: true)
                                .Build();
            Log.Logger = new LoggerConfiguration()
                         .Enrich.WithCorrelationIdHeader(headerKey: "CorrelationId")
                         .Enrich.FromLogContext()
                         .Enrich.WithExceptionDetails()
                         .Enrich.WithMachineName()
                         //.WriteTo.Debug()
#if DEBUG
                         .WriteTo.Console()
#endif
                         .WriteTo.Async(configure: x => x.File(path: $"Logs\\{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(oldValue: ".", newValue: "-")}-serviceLog-.log",
                                                               retainedFileCountLimit: 7,
                                                               rollOnFileSizeLimit: true,
                                                               fileSizeLimitBytes: 256 * 1024 * 1024,
                                                               outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{CorrelationId}] [{Level}] {Message}{NewLine}{Exception}",
                                                               rollingInterval: RollingInterval.Day)
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
                        $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(oldValue: ".", newValue: "-")}-{environment?.ToLower().Replace(oldValue: ".", newValue: "-")}-{{0:yyyy-MM}}"
                };
        }

        private static void CreateHost(string[] args, bool isService)
        {
            try
            {
                IWebHost host = CreateWebHostBuilder(args: args).Build();
                if (isService)
                {
                    host.RunAsService();
                }
                else
                {
                    host.Run();
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(messageTemplate: $"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}",
                          propertyValue: ex);
                throw;
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(configureDelegate: configuration =>
                {
                    configuration.AddJsonFile(path: "appsettings.json",
                                              optional: false,
                                              reloadOnChange: true);
                    configuration.AddJsonFile(path: Path.Combine(path1: "Configuration",
                                                                 path2: "serviceDiscovery.json"),
                                              optional: true,
                                              reloadOnChange: true);
                }).UseSerilog();
    }
}

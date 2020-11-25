using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Authentication;

namespace MainGateway
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ServicePointManager.DefaultConnectionLimit = int.MaxValue;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

            //configure logging first
            ConfigureLogging(args);

            //then create the host, so that if the host fails we can log errors
            CreateHost(args: args);
        }


        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args: args)
                       .ConfigureAppConfiguration(configureDelegate: (host,
                                                                      config) =>
                       {
                           config.AddJsonFile(path: Path.Combine(path1: "configuration",
                                                                 path2: "configuration.json"));
                       })
                       .ConfigureWebHostDefaults(configure: webBuilder =>
                       {
                           webBuilder.ConfigureKestrel(options: options =>
                           {
                               options.AddServerHeader = false;
                               options.ConfigureHttpsDefaults(configureOptions: httpsOptions => { httpsOptions.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13; });
                           }).UseStartup<Startup>();
                       }).UseSerilog();
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
                         .Enrich.WithClientIp()
                         .Enrich.WithClientAgent()
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
    }
}
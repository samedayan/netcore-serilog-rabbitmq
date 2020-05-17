using System;
using System.Reflection;
using Elk.Serilog.RabbitMq.Common.ProjectConst;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace Elk.Serilog.RabbitMq.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Logging();

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                Log.Error($"Program Fail: {exception.Message}");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration(configuration =>
                {
                    configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable(ProjectConst.EnvironmentVariable)}.json", optional: true);
                })
                .ConfigureLogging((hostingContext, config) => { config.ClearProviders(); })
                .UseSerilog();

        private static void Logging()
        {
            var environment = Environment.GetEnvironmentVariable(ProjectConst.EnvironmentVariable);
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable(ProjectConst.EnvironmentVariable)}.json", optional: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Environment", environment)
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Verbose()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            Log.Information($"App Started");
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
        {
            var name = Assembly.GetExecutingAssembly()?.GetName().Name;

            if (name != null)
                return new ElasticsearchSinkOptions(new Uri(configuration[ProjectConst.ElasticSettings.Uri]))
                {
                    CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                    AutoRegisterTemplate = true,
                    TemplateName = "events",
                    IndexFormat =
                        $"{name?.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.Now:dd.MM.yyyy}"
                };

            return new ElasticsearchSinkOptions
            {
                CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                AutoRegisterTemplate = true,
                TemplateName = "events",
                IndexFormat = $"{DateTime.Now:dd.MM.yyyy}"
            };
        }
    }
}

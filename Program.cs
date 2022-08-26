using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using open_tel_geneva_exporter_repro;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

var host = Host.CreateDefaultBuilder(args)
     .ConfigureLogging((context, logginConfigBuilder) =>
  {
      logginConfigBuilder.ClearProviders();
      logginConfigBuilder.AddConsole();

      string? serviceName = typeof(Program).Assembly.GetName().Name;
      string? version = typeof(Program).Assembly.GetName().Version?.ToString();
      var resourceBuilder = ResourceBuilder.CreateDefault().AddService(serviceName: serviceName, serviceVersion: version);

      logginConfigBuilder.AddOpenTelemetry(options =>
      {
          options.SetResourceBuilder(resourceBuilder);

          options.AddGenevaLogExporter(exporterOptions =>
          {
              exporterOptions.ConnectionString = "EtwSession=OpenTelemetry";
              exporterOptions.TableNameMappings = new Dictionary<string, string>()
              {
                  ["*"] = "CategoryEvent"
              };
          });

          options.AddConsoleExporter();

          options.IncludeFormattedMessage = false;
          options.ParseStateValues = true;
          options.IncludeScopes = true;
      });
  });

var app = host.Build();

var logger = app.Services.GetService<ILogger<Program>>();

app.RunAsync();

logger.ConsoleStarted(nameof(Program));
logger.ConsoleExited(nameof(Program));
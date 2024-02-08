using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Microsoft.Fabric.Provisioning.Library.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelemetryClient(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services.AddSingleton<TelemetryClient>((serviceProvider) =>
            {

                string instrumentationKey = config["ApiSettings:ApplicationInsights:InstrumentationKey"];
               // string instrumentationKey = "7f2db062-6e43-411d-999f-3d3d15c46b8f";
                var telemetryConfiguration = TelemetryConfiguration.CreateDefault();
                telemetryConfiguration.InstrumentationKey = instrumentationKey;

                DependencyTrackingTelemetryModule depModule = new DependencyTrackingTelemetryModule();
                depModule.Initialize(telemetryConfiguration);

                return new TelemetryClient(telemetryConfiguration);
            });

            return services;
        }
    }
}

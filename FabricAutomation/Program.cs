using FabricAutomation.Extensions;
using FabricAutomation.Services;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Fabric.Provisioning.Library.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var currentDirectory = Directory.GetCurrentDirectory();
var configuration = new ConfigurationBuilder()
    .SetBasePath(currentDirectory) // Set the base path to the current directory
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) // Load appsettings.json
    .Build();
builder.Services.AddTelemetryClient(configuration);
//builder.Services.AddSingleton<TelemetryClient>((serviceProvider) =>
//{
//    string instrumentationKey = configuration["ApiSettings:ApplicationInsights:InstrumentationKey"];
//    var telemetryConfiguration = TelemetryConfiguration.CreateDefault();
//    telemetryConfiguration.InstrumentationKey = instrumentationKey;

//    DependencyTrackingTelemetryModule depModule = new DependencyTrackingTelemetryModule();
//    depModule.Initialize(telemetryConfiguration);

//    return new TelemetryClient(telemetryConfiguration);
//});

builder.Services.AddScoped<IFabricService, FabricService>();
builder.Services.AddCors
    (options =>
    {
        options.AddPolicy("AllowAny",
            builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
    });
var app = builder.Build();

app.UseLogging();
app.UseException();
app.UseCors("AllowAny");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// See https://aka.ms/new-console-template for more information.
using System.CommandLine;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Logging;
using Microsoft.Fabric.Provisioning.Library.Models;
using ProvisioningLibrary = Microsoft.Fabric.Provisioning.Library;
using Microsoft.Extensions.Configuration;
using Microsoft.Fabric.Provisioning.Library.Services;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;

// Service collection for configuring dependency injection.

var serviceCollection = new ServiceCollection();

// below section is for reading the instrumentationkey from appsettings.json file

//var currentDirectory = Directory.GetCurrentDirectory();
//while (!File.Exists(Path.Combine(currentDirectory, "appsettings.json")) && currentDirectory != null)
//{
//    currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
//}


//Create IConfiguration object from appsettings.json for windows
//var configuration = new ConfigurationBuilder()
//    .AddJsonFile(Path.Combine(currentDirectory, "appsettings.json"), optional: true, reloadOnChange: true)
//    .Build();


//Create IConfiguration object from appsettings.json for Ubuntu
//var configuration = new ConfigurationBuilder()
//    .AddJsonFile("/home/kirthika/fabric_unified_deployment/FabricProvisioningClient/appsettings.json", optional: true, reloadOnChange: true)
//    .Build();


//ConfigureServices(serviceCollection,configuration);
ConfigureServices(serviceCollection);
var serviceProvider = serviceCollection.BuildServiceProvider();

// Get an instance of the operations class.
var operations = serviceProvider.GetService<ProvisioningLibrary.Operations>();
var logger = serviceProvider.GetService<ILoggerFactory>()?.CreateLogger("Microsoft.Fabric.Provisioning.Client");
var telemetryClient = serviceProvider.GetService<TelemetryClient>();
telemetryClient.TrackEvent("I am here created");
telemetryClient.Flush();

//create workspace
var tokenOption = new Option<string>(
            name: "--token",
            description: "The access token to authentice.")
{ IsRequired = true };

var payloadOption = new Option<ProvisioningLibrary.Models.WorkspaceRequest?>(
            name: "--payload",
            description: "The request payload or body.",
            parseArgument: result =>
            {
                if (result.Tokens.Count > 0)
                {
                    return JsonSerializer.Deserialize<WorkspaceRequest>(result.Tokens.FirstOrDefault().Value);
                }
                else
                {
                    result.ErrorMessage = "--payload is empty.";
                    return default;
                }
            })
{ IsRequired = true };

var correlationIdOption = new Option<string>(
            name: "--correlationId",
            description: "The correlation id associated with the operation.")
{ IsRequired = false };

var rootCommand = new RootCommand("Application for Microsoft Fabric Provisioning.");

var createCommand = new Command("create", "Creates workspace and associated workload.")
{
    tokenOption,
    payloadOption,
    correlationIdOption
};
rootCommand.AddCommand(createCommand);

createCommand.SetHandler((token, payload, correlationId) =>
{
    try
    {
        var response = payload != null ? operations?.CreateWorkspace(token, payload) : default;
        if (response != null)
        {
            logger?.LogInformation(JsonSerializer.Serialize<WorkspaceResponse>(response));

            if (telemetryClient != null)
            {
                telemetryClient.TrackTrace(message: "I am here after creating telemetryclient");
                var properties = new Dictionary<string, string>
                    {
                        { "WorkspaceId", response.DisplayName },
                        { "Operation", "CreateWorkspace" },
                        {"type", response.Type },
                        {"capacityId" , response.CapacityId }
                    };

                telemetryClient.TrackEvent("WorkspaceCreated", properties);
                telemetryClient.Flush();
            }
        }
        else
        {
            logger?.LogError(500, "No response found.");
        }
    }
    catch (Exception e)
    {
        logger?.LogError("Exception occured " + e);
    }

},
tokenOption, payloadOption, correlationIdOption);
//create item

var payloadOptionForCreateitem = new Option<ProvisioningLibrary.Models.CreateItemRequest?>(
            name: "--payload",
            description: "The request payload or body for creating item",
            parseArgument: result =>
            {
                if (result.Tokens.Count > 0)
                {
                    return JsonSerializer.Deserialize<CreateItemRequest>(result.Tokens.FirstOrDefault().Value);
                }
                else
                {
                    result.ErrorMessage = "--payload is empty.";
                    return default;
                }
            })
{ IsRequired = true };

var createItemCommand = new Command("createItem", "creates item for a workspace.")
{
    tokenOption,
    payloadOptionForCreateitem,
    correlationIdOption
};
rootCommand.AddCommand(createItemCommand);

createItemCommand.SetHandler((token, payloadOptionForCreateitem, correlationId) =>
{
    var response = payloadOptionForCreateitem != null ? operations?.CreateItem(token, payloadOptionForCreateitem) : default;
    if (response != null)
    {
        logger?.LogInformation(JsonSerializer.Serialize<CreateItemResponse>(response));
    }
    else
    {
        logger?.LogError(500, "No response found.");
    }
},
tokenOption, payloadOptionForCreateitem, correlationIdOption);

//get workspace

var workspaceIdOption = new Option<string>(
    name: "--workspaceId",
    description: "The workspace ID.")
{ IsRequired = true };


var getCommand = new Command("get", "Gets information about a workspace.")
{
    tokenOption,
    workspaceIdOption,
    correlationIdOption
};
rootCommand.AddCommand(getCommand);

getCommand.SetHandler((token, workspaceId, correlationId) =>
{
    var request = new GetWorkspaceRequest { WorkspaceId = workspaceId };
    var response = operations?.GetWorkspace(token, request, correlationId);
    if (response != null)
    {
        logger?.LogInformation(JsonSerializer.Serialize<GetWorkspaceResponse>(response));
    }
    else
    {
        logger?.LogError(500, "No response found.");
    }
},
tokenOption, workspaceIdOption, correlationIdOption);

//list worksapce


var continuationTokenOption = new Option<string>(
    name: "--continuationToken",
    description: "The continuation token for paging.")
{ IsRequired = true };

var listCommand = new Command("list", "Lists workspaces.")
{
    tokenOption,
    continuationTokenOption,
    correlationIdOption
};
rootCommand.AddCommand(listCommand);

listCommand.SetHandler((token, continuationToken, correlationId) =>
{
    var request = new ListWorksapceRequest { ContinuationToken = continuationToken };
    var response = operations?.ListWorkspace(token, request, correlationId);
    if (response != null)
    {
        logger?.LogInformation(JsonSerializer.Serialize<ListWorkspaceResponse>(response));
    }
    else
    {
        logger?.LogError(500, "Failed to list the resource.");
    }
},
tokenOption, continuationTokenOption, correlationIdOption);

return await rootCommand.InvokeAsync(args);



//static void ConfigureServices(ServiceCollection services, ConfigurationBuilder configuration) =>
 static void ConfigureServices(ServiceCollection services) =>
    services.AddLogging(config =>
    {
        config.AddDebug();
        config.AddConsole();
    })
    .Configure<LoggerFilterOptions>(options =>
    {
        options.AddFilter<DebugLoggerProvider>("FabricProvisioning", LogLevel.Information);
        options.AddFilter<ConsoleLoggerProvider>("FabricProvisioning", LogLevel.Warning);
    })
    .AddTransient<ProvisioningLibrary.Operations>()
    // .AddTelemetryClient(configuration);
    .AddTransient<TelemetryClient>((serviceProvider) =>
    {

        //  string instrumentationkey = configuration["ApiSettings:ApplicationInsights:InstrumentationKey"];
        // string instrumentationKey = config["ApiSettings:ApplicationInsights:InstrumentationKey"];
        string instrumentationKey = "7f2db062-6e43-411d-999f-3d3d15c46b8f";
        var telemetryConfiguration = TelemetryConfiguration.CreateDefault();
        telemetryConfiguration.InstrumentationKey = instrumentationKey;

        DependencyTrackingTelemetryModule depModule = new DependencyTrackingTelemetryModule();
        depModule.Initialize(telemetryConfiguration);

        return new TelemetryClient(telemetryConfiguration);
    });

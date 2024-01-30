using FabricAutomation.Filters;
using FabricAutomation.Models;
using FabricAutomation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Fabric.Provisioning.Library;
using Microsoft.Fabric.Provisioning.Library.Models;
using System.Diagnostics;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FabricAutomation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FabricController : ControllerBase
    {
        private readonly ILogger<FabricController> _logger;
        private readonly IFabricService _fabricService;
        private readonly ILogger<Operations> _loggerOpeartion;
        private readonly IConfiguration _configuration;

        public FabricController(ILogger<FabricController> logger, ILogger<Operations> loggerOperations,
            IFabricService fabricRepository)
        {
            _logger = logger;
            _fabricService = fabricRepository;
            _loggerOpeartion = loggerOperations;
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        [HttpPost("CreateWorkspace")]
        [ValidateRequest]
        public FabricResource CreateWorkspace(FabricResource resource)
        {
            try
            {
                var operations = new Operations(_loggerOpeartion);

                _logger.LogInformation($"Creating resource {resource.DisplayName}.");


                string token = _configuration["ApiSettings:Token"];
                string correlationId = "your_correlation_id";

                // Call the Create method from the Operations class
                var workspaceResponse = operations.CreateWorkspace(token, ConvertToCreateWorkspaceRequest(resource), correlationId);

                if (workspaceResponse != null)
                {

                    return new FabricResource
                    {
                        Id = workspaceResponse.Id,
                        DisplayName = workspaceResponse.DisplayName,
                        Description = workspaceResponse.Description,
                        Type = workspaceResponse.Type,
                       CapacityId = workspaceResponse.CapacityId,
                    };
                }
                else
                {
                    _logger?.LogError(500, "Failed to create the resource.");
                    return null;
                }
            }
            catch (Exception ex)
            {

                _logger?.LogError(500, ex, message: ex.Message);
                return null;
            }
        }

        private WorkspaceRequest ConvertToCreateWorkspaceRequest(FabricResource fabricResource)
        {

            return new WorkspaceRequest
            {
                DisplayName = fabricResource.DisplayName,
                Description = fabricResource.Description,
                CapacityId=fabricResource.CapacityId

            };
        }

        [HttpGet("GetWorkspace")]
        [ValidateRequest]
        public GetWorkspaceResponse GetWorkspace([FromQuery] GetWorkspaceRequest resource)
        {
            try
            {
                var operations = new Operations(_loggerOpeartion);

                _logger.LogInformation($"Getting resource.");

                string token = _configuration["ApiSettings:Token"];
                string correlationId = "your_correlation_id";

                // Call the Create method from the Operations class
                var getWorkspaceResponse = operations.GetWorkspace(token, resource, correlationId);

                if (getWorkspaceResponse != null)
                {
                    return new GetWorkspaceResponse
                    {
                        Id = getWorkspaceResponse.Id,
                        DisplayName = getWorkspaceResponse.DisplayName,
                        Description = getWorkspaceResponse.Description,
                        Type = getWorkspaceResponse.Type,
                        capacityAssignmentProgress = getWorkspaceResponse.capacityAssignmentProgress,
                    };
                }
                else
                {
                    _logger?.LogError(500, "Failed to create the resource.");
                    return null;
                }
            }
            catch (Exception ex)
            {

                _logger?.LogError(500, ex, message: ex.Message);
                return null;
            }
        }

        [HttpGet("ListWorkspaces")]
        [ValidateRequest]
        public ListWorkspaceResponse ListWorkspace([FromQuery] ListWorksapceRequest resource)
        {
            try
            {
                var operations = new Operations(_loggerOpeartion);

                _logger.LogInformation($"Getting list of worksapces.");

                string token = _configuration["ApiSettings:Token"];
                string correlationId = "your_correlation_id";

                // Call the Create method from the Operations class
                var listWorkspaceResponse = operations.ListWorkspace(token, resource, correlationId);

                if (listWorkspaceResponse != null)
                {
                    return new ListWorkspaceResponse
                    {
                        ContinuationToken = !string.IsNullOrWhiteSpace(listWorkspaceResponse.ContinuationToken) ? listWorkspaceResponse.ContinuationToken : "",
                        ContinuationUri = listWorkspaceResponse.ContinuationUri,
                        Value = listWorkspaceResponse.Value
                    };
                }
                else
                {
                    _logger?.LogError(500, "Failed to list the resources.");
                    return null;
                }
            }
            catch (Exception ex)
            {

                _logger?.LogError(500, ex, message: ex.Message);
                return null;
            }
        }

        [HttpDelete("DeleteWorkspace")]
        [ValidateRequest]
        public DeleteWorkspaceResponse DeleteWorkspace([FromQuery] DeleteWorkspaceRequest resource)
        {
            try
            {
                var operations = new Operations(_loggerOpeartion);

                _logger.LogInformation($"Deleting resource");

                string token = _configuration["ApiSettings:Token"];
                string correlationId = "your_correlation_id";

                // Call the Create method from the Operations class
                var deleteWorkspaceResponse = operations.DeleteWorkspace(token, resource, correlationId);

                if (deleteWorkspaceResponse != null)
                {
                    return new DeleteWorkspaceResponse
                    {
                        StatusCode = deleteWorkspaceResponse.StatusCode
                    };
                }
                else
                {
                    _logger?.LogError(500, "Failed to create the resource.");
                    return null;
                }
            }
            catch (Exception ex)
            {

                _logger?.LogError(500, ex, message: ex.Message);
                return null;
            }
        }

        [HttpPost("CreateItem")]
        [ValidateRequest]
        public CreateItemResponse CreateItem(CreateItemRequest resource)
        {
            try
            {
                var operations = new Operations(_loggerOpeartion);

                _logger.LogInformation($"Creating item {resource.DisplayName}.");


                string token = _configuration["ApiSettings:Token"];
                string correlationId = "your_correlation_id";

                // Call the Create method from the Operations class
                var createItemResponse = operations.CreateItem(token, resource, correlationId);

                if (createItemResponse != null)
                {
                    return new CreateItemResponse
                    {

                        Description = !string.IsNullOrEmpty(createItemResponse.Description) ? createItemResponse.Description : "",
                        DisplayName = !string.IsNullOrEmpty(createItemResponse.DisplayName) ? createItemResponse.DisplayName : "",
                        ID = !string.IsNullOrWhiteSpace(createItemResponse.ID) ? createItemResponse.ID : "",
                        Type = !string.IsNullOrEmpty(createItemResponse.Type) ? createItemResponse.Type : "",
                        WorkspaceId = !string.IsNullOrEmpty(createItemResponse.WorkspaceId) ? createItemResponse.WorkspaceId : ""
                    };
                }
                else
                {
                    _logger?.LogError(500, "Failed to create the resource.");
                    return null;
                }
            }
            catch (Exception ex)
            {

                _logger?.LogError(500, ex, message: ex.Message);
                return null;
            }
        }

        [HttpGet("GetItem")]
        [ValidateRequest]
        public GetItemResponse GetItem([FromQuery] GetItemRequest resource)
        {
            try
            {
                var operations = new Operations(_loggerOpeartion);

                _logger.LogInformation($"getting item");


                string token = _configuration["ApiSettings:Token"];
                string correlationId = "your_correlation_id";

                // Call the Create method from the Operations class
                var getItemResponse = operations.GetItem(token, resource, correlationId);

                if (getItemResponse != null)
                {
                    return new GetItemResponse
                    {

                        Description = !string.IsNullOrEmpty(getItemResponse.Description) ? getItemResponse.Description : "",
                        DisplayName = !string.IsNullOrEmpty(getItemResponse.DisplayName) ? getItemResponse.DisplayName : "",
                        Id = !string.IsNullOrWhiteSpace(getItemResponse.Id) ? getItemResponse.Id : "",
                        Type = !string.IsNullOrEmpty(getItemResponse.Type) ? getItemResponse.Type : "",
                        WorkspaceId = !string.IsNullOrEmpty(getItemResponse.WorkspaceId) ? getItemResponse.WorkspaceId : ""
                    };
                }
                else
                {
                    _logger?.LogError(500, "Failed to create the resource.");
                    return null;
                }
            }
            catch (Exception ex)
            {

                _logger?.LogError(500, ex, message: ex.Message);
                return null;
            }
        }

        [HttpGet("ListItems")]
        [ValidateRequest]
        public ListItemResponse ListItem([FromQuery] ListItemRequest resource)
        {
            try
            {
                var operations = new Operations(_loggerOpeartion);

                _logger.LogInformation($"listing item");

                string token = _configuration["ApiSettings:Token"];
                string correlationId = "your_correlation_id";

                // Call the Create method from the Operations class
                var listItemResponse = operations.ListItems(token, resource, correlationId);

                if (listItemResponse != null)
                {
                    return new ListItemResponse
                    {
                        ContinuationToken = listItemResponse.ContinuationToken,
                        ContinuationUri = listItemResponse.ContinuationUri,
                        Value = listItemResponse.Value
                    };
                }
                else
                {
                    _logger?.LogError(500, "Failed to list the items.");
                    return null;
                }
            }
            catch (Exception ex)
            {

                _logger?.LogError(500, ex, message: ex.Message);
                return null;
            }
        }

        [HttpPatch("UpdateItem")]
        [ValidateRequest]
        public UpdateItemResponse UpdateItem(UpdateItemRequest resource)
        {
            try
            {
                var operations = new Operations(_loggerOpeartion);

                _logger.LogInformation($"updating item");


                string token = _configuration["ApiSettings:Token"];
                string correlationId = "your_correlation_id";

                // Call the Create method from the Operations class
                var updateItemResponse = operations.UpdateItem(token, resource, correlationId);

                if (updateItemResponse != null)
                {
                    return new UpdateItemResponse
                    {

                        Description = !string.IsNullOrEmpty(updateItemResponse.Description) ? updateItemResponse.Description : "",
                        DisplayName = !string.IsNullOrEmpty(updateItemResponse.DisplayName) ? updateItemResponse.DisplayName : "",
                        ID = !string.IsNullOrWhiteSpace(updateItemResponse.ID) ? updateItemResponse.ID : "",
                        Type = !string.IsNullOrEmpty(updateItemResponse.Type) ? updateItemResponse.Type : "",
                        WorkspaceId = !string.IsNullOrEmpty(updateItemResponse.WorkspaceId) ? updateItemResponse.WorkspaceId : ""
                    };
                }
                else
                {
                    _logger?.LogError(500, "Failed to update the item.");
                    return null;
                }
            }
            catch (Exception ex)
            {

                _logger?.LogError(500, ex, message: ex.Message);
                return null;
            }
        }

        [HttpDelete("DeleteItem")]
        [ValidateRequest]
        public DeleteItemResponse DeleteItem([FromQuery] DeleteItemRequest resource)
        {
            try
            {
                var operations = new Operations(_loggerOpeartion);

                _logger.LogInformation($"deleting item");

                string token = _configuration["ApiSettings:Token"];
                string correlationId = "your_correlation_id";

                // Call the Create method from the Operations class
                var deleteItemResponse = operations.DeleteItem(token, resource, correlationId);

                if (deleteItemResponse != null)
                {
                    return new DeleteItemResponse
                    {
                        StatusCode = deleteItemResponse.StatusCode
                    };
                }
                else
                {
                    _logger?.LogError(500, "Failed to delete the item.");
                    return null;
                }
            }
            catch (Exception ex)
            {

                _logger?.LogError(500, ex, message: ex.Message);
                return null;
            }
        }
        [HttpPost("CallMixin")]
        public IActionResult CallMixin([FromBody] MixinRequest mixinRequest)
        {
            try
            {
                _logger.LogInformation("Calling the Mixin code");



                string workspaceDisplayName = mixinRequest.WorkspaceDisplayName;
                string workspaceDescription = mixinRequest.WorkspaceDescription;
                string itemDisplayName = mixinRequest.ItemDisplayName;
                string itemType=mixinRequest.ItemType;
                string workspaceId = !string.IsNullOrEmpty(mixinRequest.WorkspaceId)? mixinRequest.WorkspaceId : "abcd";
                string capacityId=mixinRequest.CapacityId;
                string token = _configuration["ApiSettings:Token"];
                string cnabFilePath = _configuration["ApiSettings:cnabFilePath"];

                // Build the command
                string command = $"porter install --param token=\"{token}\" --param workspaceDisplayName=\"{workspaceDisplayName}\" --param workspaceDescription=\"{workspaceDescription}\" --param itemDisplayName=\"{itemDisplayName}\" --param itemType=\"{itemType}\" --param workspaceId=\"{workspaceId}\" --param capacityId=\"{capacityId}\" --cnab-file {cnabFilePath} --force";

                // Start the process
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = "bash",
                    Arguments = $"-c \"{command}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = processStartInfo })
                {
                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    //string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                
                    _logger.LogInformation($"Command Output: {output}");
                    // _logger.LogError($"Command Error: {error}");

                   // return Ok(new { Output = output, Error = error });

                    return Ok(new { Output = output });
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(500, ex, message: ex.Message);
                return StatusCode(500, new { Error = ex.Message });
            }
        }

    }
}

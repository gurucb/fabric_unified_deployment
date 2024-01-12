    using FabricAutomation.Filters;
    using FabricAutomation.Models;
    using FabricAutomation.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Fabric.Provisioning.Library;
    using Microsoft.Fabric.Provisioning.Library.Models;

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
                            capacityAssignmentProgress= getWorkspaceResponse.capacityAssignmentProgress,
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
          
            [HttpGet("ListWorkspace")]
            [ValidateRequest]
            public ListWorkspaceResponse ListWorkspace(ListWorksapceRequest resource)
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
                            ContinuationToken= listWorkspaceResponse.ContinuationToken,
                            ContinuationUri= listWorkspaceResponse.ContinuationUri,
                            Value= listWorkspaceResponse.Value
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
       
            [HttpDelete]
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
                           StatusCode= deleteWorkspaceResponse.StatusCode
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

                                    Description = !string.IsNullOrEmpty(createItemResponse.Description) ? createItemResponse.Description:"",
                                    DisplayName = !string.IsNullOrEmpty(createItemResponse.DisplayName)?createItemResponse.DisplayName:"",
                                    ID = !string.IsNullOrWhiteSpace(createItemResponse.ID)? createItemResponse.ID:"",
                                    Type = !string.IsNullOrEmpty(createItemResponse.Type)? createItemResponse.Type:"",
                                    WorkspaceId = !string.IsNullOrEmpty(createItemResponse.WorkspaceId)?createItemResponse.WorkspaceId:""
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

            [HttpPatch("UpdateItem")]
            [ValidateRequest]
            public UpdateItemResponse UpdateItem( UpdateItemRequest resource)
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
    }
    }

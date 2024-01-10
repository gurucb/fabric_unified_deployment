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


        [HttpPost]
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
                var workspaceResponse = operations.CreateWorkspace(token, ConvertToWorkspaceRequest(resource), correlationId);

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

        private WorkspaceRequest ConvertToWorkspaceRequest(FabricResource fabricResource)
        {

            return new WorkspaceRequest
            {
                DisplayName = fabricResource.DisplayName,
                Description = fabricResource.Description,

            };
        }

    }
}

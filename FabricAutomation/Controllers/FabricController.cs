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

        public FabricController(ILogger<FabricController> logger, ILogger<Operations> loggerOperations,
            IFabricService fabricRepository)
        {
            _logger = logger;
            _fabricService = fabricRepository;
            _loggerOpeartion = loggerOperations;
        }


        [HttpPost]
        [ValidateRequest]
        public FabricResource Create(FabricResource resource)
        {
            try
            {
                var operations = new Operations(_loggerOpeartion);

                _logger.LogInformation($"Creating resource {resource.DisplayName}.");


                string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IjVCM25SeHRRN2ppOGVORGMzRnkwNUtmOTdaRSIsImtpZCI6IjVCM25SeHRRN2ppOGVORGMzRnkwNUtmOTdaRSJ9.eyJhdWQiOiJodHRwczovL2FwaS5mYWJyaWMubWljcm9zb2Z0LmNvbSIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzcyZjk4OGJmLTg2ZjEtNDFhZi05MWFiLTJkN2NkMDExZGI0Ny8iLCJpYXQiOjE3MDQ4ODEwNzQsIm5iZiI6MTcwNDg4MTA3NCwiZXhwIjoxNzA0ODg2NzQ3LCJhY2N0IjowLCJhY3IiOiIxIiwiYWlvIjoiQVlRQWUvOFZBQUFBL0gvRlhwcDhkbzlLSitXbGlaZ0JWYmFCVndhL3lEc29wV3hsVkRRZlE0SHFrU045TEhpRWRsd3g1ZW9MWS95aDBqZ0hZTFdaTzVmUlIvZWgrNEJYQTAwcldVa01jNlBqRU44TGVjWlJCU0RmVDFIRG9XbVNnallJQnVjcUhiczlKdGFMdmNmZHlPMWdBSzMzV3JISkV4L2EvY2c4SVNYYlROR3ZQazBDWURNPSIsImFtciI6WyJwd2QiLCJyc2EiLCJtZmEiXSwiYXBwaWQiOiIxOGZiY2ExNi0yMjI0LTQ1ZjYtODViMC1mN2JmMmIzOWIzZjMiLCJhcHBpZGFjciI6IjAiLCJjb250cm9scyI6WyJhcHBfcmVzIl0sImNvbnRyb2xzX2F1ZHMiOlsiMDAwMDAwMDktMDAwMC0wMDAwLWMwMDAtMDAwMDAwMDAwMDAwIiwiMDAwMDAwMDMtMDAwMC0wZmYxLWNlMDAtMDAwMDAwMDAwMDAwIl0sImRldmljZWlkIjoiYTMxOGUyNzYtODJlNS00MzdmLTgyNjAtODlmZDM2MmM0MGRkIiwiZmFtaWx5X25hbWUiOiJSYWphZ2FuZXNoIiwiZ2l2ZW5fbmFtZSI6IktpcnRoaWthIiwiaXBhZGRyIjoiMTA2LjUxLjE4NS45NCIsIm5hbWUiOiJLaXJ0aGlrYSBSYWphZ2FuZXNoIiwib2lkIjoiYTcyNGQ2ZjgtNDc4NS00Mzc3LThlOTAtNTZkODNiNGM2MTQzIiwib25wcmVtX3NpZCI6IlMtMS01LTIxLTIxNDY3NzMwODUtOTAzMzYzMjg1LTcxOTM0NDcwNy0yNzY2NjE2IiwicHVpZCI6IjEwMDMyMDAxOUQ0RkExQkYiLCJyaCI6IjAuQVJvQXY0ajVjdkdHcjBHUnF5MTgwQkhiUndrQUFBQUFBQUFBd0FBQUFBQUFBQUFhQUVRLiIsInNjcCI6IkFwcC5SZWFkLkFsbCBDYXBhY2l0eS5SZWFkLkFsbCBDYXBhY2l0eS5SZWFkV3JpdGUuQWxsIENvbnRlbnQuQ3JlYXRlIERhc2hib2FyZC5SZWFkLkFsbCBEYXNoYm9hcmQuUmVhZFdyaXRlLkFsbCBEYXRhZmxvdy5SZWFkLkFsbCBEYXRhZmxvdy5SZWFkV3JpdGUuQWxsIERhdGFzZXQuUmVhZC5BbGwgRGF0YXNldC5SZWFkV3JpdGUuQWxsIEdhdGV3YXkuUmVhZC5BbGwgR2F0ZXdheS5SZWFkV3JpdGUuQWxsIEl0ZW0uRXhlY3V0ZS5BbGwgSXRlbS5SZWFkV3JpdGUuQWxsIEl0ZW0uUmVzaGFyZS5BbGwgUGlwZWxpbmUuRGVwbG95IFBpcGVsaW5lLlJlYWQuQWxsIFBpcGVsaW5lLlJlYWRXcml0ZS5BbGwgUmVwb3J0LlJlYWQuQWxsIFJlcG9ydC5SZWFkV3JpdGUuQWxsIFN0b3JhZ2VBY2NvdW50LlJlYWQuQWxsIFN0b3JhZ2VBY2NvdW50LlJlYWRXcml0ZS5BbGwgVGVuYW50LlJlYWQuQWxsIFRlbmFudC5SZWFkV3JpdGUuQWxsIFVzZXJTdGF0ZS5SZWFkV3JpdGUuQWxsIFdvcmtzcGFjZS5SZWFkLkFsbCBXb3Jrc3BhY2UuUmVhZFdyaXRlLkFsbCIsInNpZ25pbl9zdGF0ZSI6WyJkdmNfbW5nZCIsImR2Y19jbXAiLCJrbXNpIl0sInN1YiI6Ik5hY2hxMHpNci1hUFpFMVNuZzltaVEwZEIyTHlGSHFGR3RFd1FMcldTYkEiLCJ0aWQiOiI3MmY5ODhiZi04NmYxLTQxYWYtOTFhYi0yZDdjZDAxMWRiNDciLCJ1bmlxdWVfbmFtZSI6ImtyYWphZ2FuZXNoQG1pY3Jvc29mdC5jb20iLCJ1cG4iOiJrcmFqYWdhbmVzaEBtaWNyb3NvZnQuY29tIiwidXRpIjoiMGFoUThiRGpwVUNXZktnX0hONHBBQSIsInZlciI6IjEuMCIsIndpZHMiOlsiYjc5ZmJmNGQtM2VmOS00Njg5LTgxNDMtNzZiMTk0ZTg1NTA5Il19.gc5GzrR0jgiSG13CMDdqOTGrKDzDbDEhkVZOtho9VVdJvj9mqgt632m62N9esG2fJeM5zlgaqjghm-lCAlAek6K1VAlAgssuSOZzn6bp-cRhoo8eQJyEBcfAbbGr61mc9s7JlQ8MjRCHW1EBUjBgCh01aNs4Q5DkUAb6czFl3ADY6JGC1NFeDCcaHDqIrBwkRsJEWWXZzDAgodReT-00OC8yt1Sep9AXrDRT6vU6npXeNa2ji92TnoWbi6kFryHe9vJFsB06E_XeunhmuNLEAXsfGMeLwoids8P_Db06Phl5kfYUzkW1MOS8I-XFEhMV93R-_1oiqXB9tfKQEeTVcA";
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

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

        //[HttpPost]
        //[ValidateRequest]
        //public FabricResource Create(FabricResource resource)
        //{
        //    _logger.LogInformation($"Creating resource {resource.DisplayName}.");
        //    return _fabricService.CreateResource(resource);

        //}

        [HttpPost]
        [ValidateRequest]
        public FabricResource Create(FabricResource resource)
        {
            try
            {
                var operations = new Operations(_loggerOpeartion);

                _logger.LogInformation($"Creating resource {resource.DisplayName}.");


                string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IjVCM25SeHRRN2ppOGVORGMzRnkwNUtmOTdaRSIsImtpZCI6IjVCM25SeHRRN2ppOGVORGMzRnkwNUtmOTdaRSJ9.eyJhdWQiOiJodHRwczovL2FwaS5mYWJyaWMubWljcm9zb2Z0LmNvbSIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzcyZjk4OGJmLTg2ZjEtNDFhZi05MWFiLTJkN2NkMDExZGI0Ny8iLCJpYXQiOjE3MDQ3ODM1MjMsIm5iZiI6MTcwNDc4MzUyMywiZXhwIjoxNzA0Nzg4NDg2LCJhY2N0IjowLCJhY3IiOiIxIiwiYWlvIjoiQVlRQWUvOFZBQUFBZ0EzbnJnYW1Ea1czRTVJTS9VeWFKMmhMUmZLajc2Z3VZQmVjYmUxY0pzZWZLWGFXMlhyUGM0STNNT29pYXd5WlNneVFncGpEdnFWR1pudGVUZjFEZE14Tkc4cjRJazBadWNYWU9VdngrdlU1Z1d6Zm1GeVo3NHJ6c0JaUXJMaU4ydHJtZ1U5ZmdaaDNiaFM4OENDWFBaNTBqZE1hU3hUckZvdjQ1ekdjQSt3PSIsImFtciI6WyJyc2EiLCJtZmEiXSwiYXBwaWQiOiIxOGZiY2ExNi0yMjI0LTQ1ZjYtODViMC1mN2JmMmIzOWIzZjMiLCJhcHBpZGFjciI6IjAiLCJjb250cm9scyI6WyJhcHBfcmVzIl0sImNvbnRyb2xzX2F1ZHMiOlsiMDAwMDAwMDktMDAwMC0wMDAwLWMwMDAtMDAwMDAwMDAwMDAwIiwiMDAwMDAwMDMtMDAwMC0wZmYxLWNlMDAtMDAwMDAwMDAwMDAwIl0sImRldmljZWlkIjoiYTMxOGUyNzYtODJlNS00MzdmLTgyNjAtODlmZDM2MmM0MGRkIiwiZmFtaWx5X25hbWUiOiJSYWphZ2FuZXNoIiwiZ2l2ZW5fbmFtZSI6IktpcnRoaWthIiwiaXBhZGRyIjoiMTA2LjUxLjE4NS45NCIsIm5hbWUiOiJLaXJ0aGlrYSBSYWphZ2FuZXNoIiwib2lkIjoiYTcyNGQ2ZjgtNDc4NS00Mzc3LThlOTAtNTZkODNiNGM2MTQzIiwib25wcmVtX3NpZCI6IlMtMS01LTIxLTIxNDY3NzMwODUtOTAzMzYzMjg1LTcxOTM0NDcwNy0yNzY2NjE2IiwicHVpZCI6IjEwMDMyMDAxOUQ0RkExQkYiLCJyaCI6IjAuQVJvQXY0ajVjdkdHcjBHUnF5MTgwQkhiUndrQUFBQUFBQUFBd0FBQUFBQUFBQUFhQUVRLiIsInNjcCI6IkFwcC5SZWFkLkFsbCBDYXBhY2l0eS5SZWFkLkFsbCBDYXBhY2l0eS5SZWFkV3JpdGUuQWxsIENvbnRlbnQuQ3JlYXRlIERhc2hib2FyZC5SZWFkLkFsbCBEYXNoYm9hcmQuUmVhZFdyaXRlLkFsbCBEYXRhZmxvdy5SZWFkLkFsbCBEYXRhZmxvdy5SZWFkV3JpdGUuQWxsIERhdGFzZXQuUmVhZC5BbGwgRGF0YXNldC5SZWFkV3JpdGUuQWxsIEdhdGV3YXkuUmVhZC5BbGwgR2F0ZXdheS5SZWFkV3JpdGUuQWxsIEl0ZW0uRXhlY3V0ZS5BbGwgSXRlbS5SZWFkV3JpdGUuQWxsIEl0ZW0uUmVzaGFyZS5BbGwgUGlwZWxpbmUuRGVwbG95IFBpcGVsaW5lLlJlYWQuQWxsIFBpcGVsaW5lLlJlYWRXcml0ZS5BbGwgUmVwb3J0LlJlYWQuQWxsIFJlcG9ydC5SZWFkV3JpdGUuQWxsIFN0b3JhZ2VBY2NvdW50LlJlYWQuQWxsIFN0b3JhZ2VBY2NvdW50LlJlYWRXcml0ZS5BbGwgVGVuYW50LlJlYWQuQWxsIFRlbmFudC5SZWFkV3JpdGUuQWxsIFVzZXJTdGF0ZS5SZWFkV3JpdGUuQWxsIFdvcmtzcGFjZS5SZWFkLkFsbCBXb3Jrc3BhY2UuUmVhZFdyaXRlLkFsbCIsInNpZ25pbl9zdGF0ZSI6WyJkdmNfbW5nZCIsImR2Y19jbXAiLCJrbXNpIl0sInN1YiI6Ik5hY2hxMHpNci1hUFpFMVNuZzltaVEwZEIyTHlGSHFGR3RFd1FMcldTYkEiLCJ0aWQiOiI3MmY5ODhiZi04NmYxLTQxYWYtOTFhYi0yZDdjZDAxMWRiNDciLCJ1bmlxdWVfbmFtZSI6ImtyYWphZ2FuZXNoQG1pY3Jvc29mdC5jb20iLCJ1cG4iOiJrcmFqYWdhbmVzaEBtaWNyb3NvZnQuY29tIiwidXRpIjoiZG1MMzIxX1lHVUtMd3hBLW96OEdBQSIsInZlciI6IjEuMCIsIndpZHMiOlsiYjc5ZmJmNGQtM2VmOS00Njg5LTgxNDMtNzZiMTk0ZTg1NTA5Il19.W3Fx5UeY5TVK6ajHmJpriHVSf0xx0kw499KN1JqsAqYuM4HFOHx-7ngmpzX2-sO3trN8kLf8jemrDeN9NDWDVOxyPcSS9s7XvDQ5ae3lKYO90V8-Vc6RYC-ZFfaA1_s5iHsHKq1yYyn-qqApdbV7GKeFIm4z5nTQkI3JFlwPa5hPGz4gpuoEFmhLrX29nXutfbNuT5fRITA3aKLaTnYp9Y3cvLL9Rfo9ntYaacvW3QfHH03qMtuE-k3nhZDrxgz9M1Tg1OJcE98o6lROSYqbiApv5z1XB43oLmLU9dACjeA0WmnPgK1hMkWPyRDLm-NVbSGxxie2GH-BKwGIydT2ag";
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

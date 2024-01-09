using FabricAutomation.Controllers;
using FabricAutomation.Models;

namespace FabricAutomation.Services
{
    public class FabricService : IFabricService
    {
        public FabricResource CreateResource(FabricResource resource)
        {
            if (resource is null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            return new FabricResource
            {
                Description = resource.Description,
                DisplayName = resource.DisplayName,
                Id = resource.Id,
                Location = resource.Location,
                Type = resource.Type,
                WorkspaceId = resource.WorkspaceId,
            };
        }
    }
}

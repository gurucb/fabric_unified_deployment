namespace Microsoft.Fabric.Provisioning.Library
{
    using System.Net.Http.Json;
    using System.Net.Http.Headers;
    using System.Runtime.InteropServices;
    using Microsoft.Extensions.Logging;
    using Microsoft.Fabric.Provisioning.Library.Models;
    using System.Text.Json;

    public class Operations
    {
        private readonly ILogger<Operations> logger;
        private readonly HttpClient client;

        public Operations(ILogger<Operations> logger)
        {
            this.logger = logger;
            this.client = new();
        }

        public WorkspaceResponse? CreateWorkspace(string token, 
            WorkspaceRequest payload, 
            [Optional] string correlationId)
        {
            this.logger?.LogInformation("Invoked the 'Create Workspace' operation.");
            this.logger?.LogInformation(JsonSerializer.Serialize<WorkspaceRequest>(payload));

            this.client.BaseAddress = new Uri("https://api.fabric.microsoft.com/");
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.DefaultRequestHeaders.Add("User-Agent", "Microsoft Fabric Provisioning");
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            // Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "key", "secret"))));

            try
            {

                var responseMessage = this.client.PostAsJsonAsync<WorkspaceRequest>($"v1/workspaces", payload).Result;
                // responseMessage.EnsureSuccessStatusCode()

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content?.ReadFromJsonAsync<WorkspaceResponse>().Result;
                }
                else
                {
                    this.logger?.LogError(500, "Failed to create the resource.");
                    return default;
                }
            }
            catch (Exception ex) {
                this.logger?.LogError(500, ex, message: ex.Message);
                return default;
            }
        }

        public GetWorkspaceResponse? GetWorkspace(string token,
            GetWorkspaceRequest payload,
            [Optional] string correlationId)
        {
            this.logger?.LogInformation("Invoked the 'Get Workspace' operation.");
            this.logger?.LogInformation(JsonSerializer.Serialize<GetWorkspaceRequest>(payload));

            this.client.BaseAddress = new Uri("https://api.fabric.microsoft.com/");
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.DefaultRequestHeaders.Add("User-Agent", "Microsoft Fabric Provisioning");
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {

                var workspaceId = payload.WorkspaceId;
                var responseMessage = this.client.GetAsync($"v1/workspaces/{workspaceId}").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content?.ReadFromJsonAsync<GetWorkspaceResponse>().Result;
                }
                else
                {
                    this.logger?.LogError(500, "Failed to get the resource.");
                    return default;
                }
            }
            catch (Exception ex)
            {
                this.logger?.LogError(500, ex, message: ex.Message);
                return default;
            }
        }

        public ListWorkspaceResponse? ListWorkspace(string token,
            ListWorksapceRequest payload,
            [Optional] string correlationId)
        {
            this.logger?.LogInformation("Invoked the 'List' operation.");
            this.logger?.LogInformation(JsonSerializer.Serialize<ListWorksapceRequest>(payload));

            this.client.BaseAddress = new Uri("https://api.fabric.microsoft.com/");
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.DefaultRequestHeaders.Add("User-Agent", "Microsoft Fabric Provisioning");
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {

                var continuationToken = payload.ContinuationToken;
                var responseMessage = this.client.GetAsync($"v1/workspaces?continuationToken={continuationToken}").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content?.ReadFromJsonAsync<ListWorkspaceResponse>().Result;
                }
                else
                {
                    this.logger?.LogError(500, "Failed to list the resource.");
                    return default;
                }
            }
            catch (Exception ex)
            {
                this.logger?.LogError(500, ex, message: ex.Message);
                return default;
            }
        }


        public ListWorkspaceResponse? DeleteWorkspace(string token,
    ListWorksapceRequest payload,
    [Optional] string correlationId)
        {
            this.logger?.LogInformation("Invoked the 'List' operation.");
            this.logger?.LogInformation(JsonSerializer.Serialize<ListWorksapceRequest>(payload));

            this.client.BaseAddress = new Uri("https://api.fabric.microsoft.com/");
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.DefaultRequestHeaders.Add("User-Agent", "Microsoft Fabric Provisioning");
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {

                var continuationToken = payload.ContinuationToken;
                var responseMessage = this.client.GetAsync($"v1/workspaces?continuationToken={continuationToken}").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content?.ReadFromJsonAsync<ListWorkspaceResponse>().Result;
                }
                else
                {
                    this.logger?.LogError(500, "Failed to list the resource.");
                    return default;
                }
            }
            catch (Exception ex)
            {
                this.logger?.LogError(500, ex, message: ex.Message);
                return default;
            }
        }

        public ListWorkspaceResponse? CreateItem(string token,
            ListWorksapceRequest payload,
            [Optional] string correlationId)
        {
            this.logger?.LogInformation("Invoked the 'List' operation.");
            this.logger?.LogInformation(JsonSerializer.Serialize<ListWorksapceRequest>(payload));

            this.client.BaseAddress = new Uri("https://api.fabric.microsoft.com/");
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.DefaultRequestHeaders.Add("User-Agent", "Microsoft Fabric Provisioning");
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {

                var continuationToken = payload.ContinuationToken;
                var responseMessage = this.client.GetAsync($"v1/workspaces?continuationToken={continuationToken}").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content?.ReadFromJsonAsync<ListWorkspaceResponse>().Result;
                }
                else
                {
                    this.logger?.LogError(500, "Failed to list the resource.");
                    return default;
                }
            }
            catch (Exception ex)
            {
                this.logger?.LogError(500, ex, message: ex.Message);
                return default;
            }
        }

        public ListWorkspaceResponse? GetItem(string token,
        ListWorksapceRequest payload,
        [Optional] string correlationId)
        {
            this.logger?.LogInformation("Invoked the 'List' operation.");
            this.logger?.LogInformation(JsonSerializer.Serialize<ListWorksapceRequest>(payload));

            this.client.BaseAddress = new Uri("https://api.fabric.microsoft.com/");
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.DefaultRequestHeaders.Add("User-Agent", "Microsoft Fabric Provisioning");
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {

                var continuationToken = payload.ContinuationToken;
                var responseMessage = this.client.GetAsync($"v1/workspaces?continuationToken={continuationToken}").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content?.ReadFromJsonAsync<ListWorkspaceResponse>().Result;
                }
                else
                {
                    this.logger?.LogError(500, "Failed to list the resource.");
                    return default;
                }
            }
            catch (Exception ex)
            {
                this.logger?.LogError(500, ex, message: ex.Message);
                return default;
            }
        }

        public ListWorkspaceResponse? UpdateItem(string token,
            ListWorksapceRequest payload,
            [Optional] string correlationId)
        {
            this.logger?.LogInformation("Invoked the 'List' operation.");
            this.logger?.LogInformation(JsonSerializer.Serialize<ListWorksapceRequest>(payload));

            this.client.BaseAddress = new Uri("https://api.fabric.microsoft.com/");
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.DefaultRequestHeaders.Add("User-Agent", "Microsoft Fabric Provisioning");
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {

                var continuationToken = payload.ContinuationToken;
                var responseMessage = this.client.GetAsync($"v1/workspaces?continuationToken={continuationToken}").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content?.ReadFromJsonAsync<ListWorkspaceResponse>().Result;
                }
                else
                {
                    this.logger?.LogError(500, "Failed to list the resource.");
                    return default;
                }
            }
            catch (Exception ex)
            {
                this.logger?.LogError(500, ex, message: ex.Message);
                return default;
            }
        }

        public ListWorkspaceResponse? DeleteItem(string token,
        ListWorksapceRequest payload,
            [Optional] string correlationId)
        {
            this.logger?.LogInformation("Invoked the 'List' operation.");
            this.logger?.LogInformation(JsonSerializer.Serialize<ListWorksapceRequest>(payload));

            this.client.BaseAddress = new Uri("https://api.fabric.microsoft.com/");
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.DefaultRequestHeaders.Add("User-Agent", "Microsoft Fabric Provisioning");
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {

                var continuationToken = payload.ContinuationToken;
                var responseMessage = this.client.GetAsync($"v1/workspaces?continuationToken={continuationToken}").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content?.ReadFromJsonAsync<ListWorkspaceResponse>().Result;
                }
                else
                {
                    this.logger?.LogError(500, "Failed to list the resource.");
                    return default;
                }
            }
            catch (Exception ex)
            {
                this.logger?.LogError(500, ex, message: ex.Message);
                return default;
            }
        }

        public ListWorkspaceResponse? ListItems(string token,
    ListWorksapceRequest payload,
    [Optional] string correlationId)
        {
            this.logger?.LogInformation("Invoked the 'List' operation.");
            this.logger?.LogInformation(JsonSerializer.Serialize<ListWorksapceRequest>(payload));

            this.client.BaseAddress = new Uri("https://api.fabric.microsoft.com/");
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.DefaultRequestHeaders.Add("User-Agent", "Microsoft Fabric Provisioning");
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {

                var continuationToken = payload.ContinuationToken;
                var responseMessage = this.client.GetAsync($"v1/workspaces?continuationToken={continuationToken}").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content?.ReadFromJsonAsync<ListWorkspaceResponse>().Result;
                }
                else
                {
                    this.logger?.LogError(500, "Failed to list the resource.");
                    return default;
                }
            }
            catch (Exception ex)
            {
                this.logger?.LogError(500, ex, message: ex.Message);
                return default;
            }
        }
    }
}

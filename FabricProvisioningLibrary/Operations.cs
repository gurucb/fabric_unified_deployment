﻿namespace Microsoft.Fabric.Provisioning.Library
{
    using System.Net.Http.Json;
    using System.Net.Http.Headers;
    using System.Runtime.InteropServices;
    using Microsoft.Extensions.Logging;
    using Microsoft.Fabric.Provisioning.Library.Models;
    using System.Text.Json;
    using System.Runtime.CompilerServices;

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
                    this.logger?.LogError(500, "Failed to create the workspace resource.");
                    return default;
                }
            }
            catch (Exception ex)
            {
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
                    this.logger?.LogError(500, "Failed to get the workspace resource.");
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

                var continuationToken = !string.IsNullOrWhiteSpace(payload.ContinuationToken)? payload.ContinuationToken:"";
                var responseMessage = default(HttpResponseMessage); 
                if (!string.IsNullOrWhiteSpace(continuationToken))
                {
                    responseMessage = this.client.GetAsync($"v1/workspaces?continuationToken={continuationToken}").Result;
                }
                else
                {
                    responseMessage = this.client.GetAsync($"v1/workspaces").Result;
                }

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content?.ReadFromJsonAsync<ListWorkspaceResponse>().Result;
                }
                else
                {
                    this.logger?.LogError(500, "Failed to list the workspace resource.");
                    return default;
                }
            }
            catch (Exception ex)
            {
                this.logger?.LogError(500, ex, message: ex.Message);
                return default;
            }
        }


        public DeleteWorkspaceResponse? DeleteWorkspace(string token,
        DeleteWorkspaceRequest payload,
        [Optional] string correlationId)
        {
            this.logger?.LogInformation("Invoked the 'List' operation.");
            this.logger?.LogInformation(JsonSerializer.Serialize<DeleteWorkspaceRequest>(payload));

            this.client.BaseAddress = new Uri("https://api.fabric.microsoft.com/");
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.DefaultRequestHeaders.Add("User-Agent", "Microsoft Fabric Provisioning");
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {

                var workspaceId = payload.WorkspaceId;
                var responseMessage = this.client.DeleteAsync($"v1/workspaces/{workspaceId}").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content?.ReadFromJsonAsync<DeleteWorkspaceResponse>().Result;
                }
                else
                {
                    this.logger?.LogError(500, "Failed to delete the workspace resource.");
                    return default;
                }
            }
            catch (Exception ex)
            {
                this.logger?.LogError(500, ex, message: ex.Message);
                return default;
            }
        }

        public CreateItemResponse? CreateItem(string token,
            CreateItemRequest payload,
            [Optional] string correlationId)
        {
            this.logger?.LogInformation("Invoked the 'create item' operation.");
            this.logger?.LogInformation(JsonSerializer.Serialize<CreateItemRequest>(payload));

            this.client.BaseAddress = new Uri("https://api.fabric.microsoft.com/");
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.DefaultRequestHeaders.Add("User-Agent", "Microsoft Fabric Provisioning");
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {

                var workspaceId = payload.WorkspaceId;
                var responseMessage = this.client.PostAsJsonAsync<CreateItemRequest>($"v1/workspaces/{workspaceId}/items", payload).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content?.ReadFromJsonAsync<CreateItemResponse>().Result;
                }
                else
                {
                    this.logger?.LogError(500, "Failed to create the item resource.");
                    return default;
                }
            }
            catch (Exception ex)
            {
                this.logger?.LogError(500, ex, message: ex.Message);
                return default;
            }
        }

        public GetItemResponse? GetItem(string token,
        GetItemRequest payload,
        [Optional] string correlationId)
        {
            this.logger?.LogInformation("Invoked the 'List' operation.");
            this.logger?.LogInformation(JsonSerializer.Serialize<GetItemRequest>(payload));

            this.client.BaseAddress = new Uri("https://api.fabric.microsoft.com/");
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.DefaultRequestHeaders.Add("User-Agent", "Microsoft Fabric Provisioning");
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {

                var workspaceId = payload.WorkspaceId;
                var itemId = payload.itemId;
                var responseMessage = this.client.GetAsync($"v1/workspaces/{workspaceId}/items/{itemId}").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content?.ReadFromJsonAsync<GetItemResponse>().Result;
                }
                else
                {
                    this.logger?.LogError(500, "Failed to get the item resource.");
                    return default;
                }
            }
            catch (Exception ex)
            {
                this.logger?.LogError(500, ex, message: ex.Message);
                return default;
            }
        }

        public UpdateItemResponse? UpdateItem(string token,
            UpdateItemRequest payload,
            [Optional] string correlationId)
        {
            this.logger?.LogInformation("Invoked the 'List' operation.");
            this.logger?.LogInformation(JsonSerializer.Serialize<UpdateItemRequest>(payload));

            this.client.BaseAddress = new Uri("https://api.fabric.microsoft.com/");
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.DefaultRequestHeaders.Add("User-Agent", "Microsoft Fabric Provisioning");
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {

                var workspaceId = payload.WorkspaceId;
                var itemId = payload.ItemId;
                var responseMessage = this.client.PatchAsJsonAsync($"v1/workspaces/{workspaceId}/items/{itemId}", payload).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content?.ReadFromJsonAsync<UpdateItemResponse>().Result;
                }
                else
                {
                    this.logger?.LogError(500, "Failed to update the item resource.");
                    return default;
                }
            }
            catch (Exception ex)
            {
                this.logger?.LogError(500, ex, message: ex.Message);
                return default;
            }
        }

        public DeleteItemResponse? DeleteItem(string token,
        DeleteItemRequest payload,
            [Optional] string correlationId)
        {
            this.logger?.LogInformation("Invoked the 'List' operation.");
            this.logger?.LogInformation(JsonSerializer.Serialize<DeleteItemRequest>(payload));

            this.client.BaseAddress = new Uri("https://api.fabric.microsoft.com/");
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.DefaultRequestHeaders.Add("User-Agent", "Microsoft Fabric Provisioning");
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                var workspaceId = payload.WorkspaceId;
                var itemId = payload.itemId;
                var responseMessage = this.client.DeleteAsync($"v1/workspaces/{workspaceId}/items/{itemId}").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content?.ReadFromJsonAsync<DeleteItemResponse>().Result;
                }
                else
                {
                    this.logger?.LogError(500, "Failed to delete the item resource.");
                    return default;
                }
            }
            catch (Exception ex)
            {
                this.logger?.LogError(500, ex, message: ex.Message);
                return default;
            }
        }

        public ListItemResponse? ListItems(string token,
        ListItemRequest payload,
        [Optional] string correlationId)
        {
            this.logger?.LogInformation("Invoked the 'List' item operation.");
            this.logger?.LogInformation(JsonSerializer.Serialize<ListItemRequest>(payload));

            this.client.BaseAddress = new Uri("https://api.fabric.microsoft.com/");
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.DefaultRequestHeaders.Add("User-Agent", "Microsoft Fabric Provisioning");
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {

                var workspaceId = payload.WorkspaceId;
                var type = !string.IsNullOrWhiteSpace(payload.Type)?payload.Type:"";
                var continuationToken = !string.IsNullOrWhiteSpace(payload.ContinuationToken)? payload.ContinuationToken:"";
                var responseMessage = default(HttpResponseMessage);
                if (!string.IsNullOrWhiteSpace(type) && !string.IsNullOrEmpty(continuationToken)) {
                     responseMessage = this.client.GetAsync($"v1/workspaces/{workspaceId}/items?type={type}&continuationToken={continuationToken}").Result;
                }
                else
                {
                     responseMessage = this.client.GetAsync($"v1/workspaces/{workspaceId}/items").Result;
                }
                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content?.ReadFromJsonAsync<ListItemResponse>().Result;
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

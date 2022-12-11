using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IdentityServer.MultiTenant.OpenApi
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            // Add Tenant Key parameter so users have to provide the key to access the
            // appropriate tenant partition
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "TenantKey",
                In = ParameterLocation.Header,
                Description = "Tenant Key",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString("Tenant1")
                }
            });
        }
    }
}

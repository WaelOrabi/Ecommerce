using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class HidePropertiesFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var parameters = operation.Parameters.ToList();
        foreach (var param in parameters)
        {
            if (param.Name == "Images") // Replace with the actual property name
            {
                operation.Parameters.Remove(param);
            }
        }
    }
}

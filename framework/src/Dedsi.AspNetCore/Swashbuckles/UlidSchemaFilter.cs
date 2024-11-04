using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dedsi.AspNetCore.Swashbuckles;

/// <summary>
/// Ulid 在 swagger 中显示
/// </summary>
public class UlidSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(Ulid))
        {
            schema.Type = "string";
            schema.Format = "string";
        }
    }
}
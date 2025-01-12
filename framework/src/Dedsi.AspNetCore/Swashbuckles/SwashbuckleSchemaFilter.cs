using Dedsi.Ddd.Domain.Shared.EntityIds;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dedsi.AspNetCore.Swashbuckles;

/// <summary>
/// Ulid 在 swagger 中显示
/// </summary>
public class SwashbuckleSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(IInt64StronglyTypedId))
        {
            schema.Type = "string";
            schema.Format = "string";
        }
        
        if (context.Type == typeof(IGuidStronglyTypedId))
        {
            schema.Type = "string";
            schema.Format = "string";
        }
        
        if (context.Type == typeof(IUlidStronglyTypedId))
        {
            schema.Type = "string";
            schema.Format = "string";
        }
    }
}
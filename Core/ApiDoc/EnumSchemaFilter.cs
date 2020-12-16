using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace Core.ApiDoc
{
    /// <summary>
    /// API'lerde Enum değerlerini ve isimlerini doğru gödermek için yapılan eklenti.
    /// </summary>
    internal class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                var enumValues = schema.Enum.ToArray();
                var i = 0;
                schema.Enum.Clear();
                foreach (var n in Enum.GetNames(context.Type).ToList())
                {
                    schema.Enum.Add(new OpenApiString(n));
                    schema.Title = ((OpenApiPrimitive<int>)enumValues[i]).Value.ToString();
                    i++;
                }
            }
        }
    }
}

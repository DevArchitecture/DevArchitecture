using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Utilities.Converters;
public class JsonDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.Parse(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Kind == DateTimeKind.Utc 
            ? value.ToLocalTime().ToString() 
            : value.ToString());
    }
}

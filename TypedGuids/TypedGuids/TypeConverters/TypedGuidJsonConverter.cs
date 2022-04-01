using System.Text.Json;
using System.Text.Json.Serialization;

namespace TypedGuids.TypeConverters;

/// <summary>
///     Used to create a JsonConverter for a TypedGuid
/// </summary>
public class TypedGuidJsonConverterFactory : JsonConverterFactory
{
    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsGenericType &&
               typeToConvert.GetGenericTypeDefinition() == typeof(TypedGuid<>) &&
               typeToConvert.GetGenericArguments().Length == 1;
    }

    /// <inheritdoc />
    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        return Activator.CreateInstance(
            typeof(TypedGuidJsonConverter<>).MakeGenericType(typeToConvert.GetGenericArguments()[0])
        ) as JsonConverter;
    }
}

/// <summary>
///     TypedGuid JsonConverter for TType
/// </summary>
/// <typeparam name="TType">Required TType</typeparam>
public class TypedGuidJsonConverter<TType> : JsonConverter<TypedGuid<TType>>
{
    /// <inheritdoc />
    public override TypedGuid<TType>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType == JsonTokenType.String
            ? new TypedGuid<TType>(reader.GetString() ?? Guid.Empty.ToString())
            : null;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, TypedGuid<TType> value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
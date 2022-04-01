using System.ComponentModel;
using System.Globalization;

namespace TypedGuids.TypeConverters;

/// <summary>
///     Used to parse TypedGuids from strings and other guids
/// </summary>
public class TypedGuidTypeConverter : TypeConverter
{
    private readonly Type type;

    /// <inheritdoc />
    public TypedGuidTypeConverter(Type type)
    {
        if (!type.IsGenericType ||
            type.GetGenericTypeDefinition() != typeof(TypedGuid<>) ||
            type.GetGenericArguments().Length != 1)
            throw new ArgumentException("Invalid type specified for TypedGuidTypeConverter {Type}", type.FullName);

        this.type = type;
    }

    /// <inheritdoc />
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || sourceType == typeof(Guid);
    }

    /// <inheritdoc />
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        return destinationType == typeof(string);
    }

    /// <inheritdoc />
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        return Activator.CreateInstance(type, value);
    }

    /// <inheritdoc />
    public override object? ConvertTo(
        ITypeDescriptorContext? context,
        CultureInfo? culture,
        object? value,
        Type destinationType
    )
    {
        return value?.ToString();
    }
}
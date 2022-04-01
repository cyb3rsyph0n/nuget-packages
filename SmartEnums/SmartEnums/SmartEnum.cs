using Abstractions;
using SmartEnums.Exceptions;

namespace SmartEnums;

/// <summary>
///     Enum class for holding a name and value with operations to find by name
/// </summary>
/// <typeparam name="TType">Type that is being inherited</typeparam>
public abstract class SmartEnum<TType> : ISmartEnum where TType : SmartEnum<TType>
{
    /// <summary>
    ///     Constructor that sets the name and description
    /// </summary>
    /// <param name="name">Required name for base enum</param>
    /// <param name="description">Required description for base enum</param>
    protected SmartEnum(string name, string description)
    {
        Name = name;
        Description = description;
    }

    /// <summary>
    ///     Description of the enum being created
    /// </summary>
    public string Description { get; }

    /// <summary>
    ///     Name of the enum being created
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Overrides equals
    /// </summary>
    /// <param name="obj">Required obj to compare to</param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;

        if (obj is string) return Name.Equals(obj.ToString(), StringComparison.InvariantCultureIgnoreCase);

        return obj.GetType() == GetType() && Equals((SmartEnum<TType>) obj);
    }

    /// <summary>
    ///     Check equals against a string
    /// </summary>
    /// <param name="value">Required value to check against</param>
    /// <param name="comparisonType">Comparison type</param>
    /// <returns></returns>
    public bool Equals(string value, StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
    {
        return Name.Equals(value, comparisonType);
    }

    /// <summary>
    ///     Looks up an enum of the inherited type by its name
    /// </summary>
    /// <param name="name">name of the enum to look for</param>
    /// <returns>Enum class that has the matching name</returns>
    public static TType? FindByName(string? name)
    {
        var pList = typeof(TType).GetFields();

        if (string.IsNullOrEmpty(name)) return null;

        foreach (var p in pList)
            if (p.FieldType.IsAssignableFrom(typeof(TType)))
            {
                var b = (ISmartEnum) p.GetValue(null)!;

                if (b?.Name == name) return (TType) b;
            }

        throw new EnumNotFoundException($"{typeof(TType).Name} does not contain a value for {name}");
    }

    /// <summary>
    ///     Override for GetHashCode
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Description, Name);
    }

    /// <summary>
    ///     Override == operator
    /// </summary>
    /// <param name="lhs">Required lhs to compare</param>
    /// <param name="rhs">Required rhs to compare</param>
    /// <returns></returns>
    public static bool operator ==(SmartEnum<TType> lhs, SmartEnum<TType> rhs)
    {
        return rhs?.Name == lhs?.Name && rhs?.Description == lhs?.Description;
    }

    /// <summary>
    ///     Override == operator for comparing strings
    /// </summary>
    /// <param name="lhs">BaseEnum to compare</param>
    /// <param name="rhs">string name to compare against</param>
    /// <returns></returns>
    public static bool operator ==(SmartEnum<TType> lhs, string rhs)
    {
        return string.Equals(lhs?.Name, rhs, StringComparison.CurrentCultureIgnoreCase);
    }

    /// <summary>
    ///     Implicit conversion for string to BaseEnum
    /// </summary>
    /// <param name="s">Required name to convert from</param>
    /// <returns></returns>
    public static implicit operator SmartEnum<TType>?(string? s)
    {
        return s == null ? null : FindByName(s);
    }

    /// <summary>
    ///     Override != operator for comparing string
    /// </summary>
    /// <param name="lhs">BaseEnum to compare</param>
    /// <param name="rhs">string name to compare against</param>
    /// <returns></returns>
    public static bool operator !=(SmartEnum<TType> lhs, string rhs)
    {
        return !string.Equals(lhs?.Name, rhs, StringComparison.CurrentCultureIgnoreCase);
    }

    /// <summary>
    ///     Override != operator
    /// </summary>
    /// <param name="lhs">Required lhs to compare</param>
    /// <param name="rhs">Required rhs to compare</param>
    /// <returns></returns>
    public static bool operator !=(SmartEnum<TType> lhs, SmartEnum<TType> rhs)
    {
        return !(lhs == rhs);
    }

    /// <summary>
    ///     Return properties as a key value pair
    /// </summary>
    /// <returns></returns>
    public virtual Dictionary<string, string> ToDictionary()
    {
        return new Dictionary<string, string> { { "Value", Name }, { "Text", Description } };
    }

    /// <summary>
    ///     Returns the Description of the enum created
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Description;
    }

    /// <summary>
    ///     Override equals
    /// </summary>
    /// <param name="other">Required base enum to compare to</param>
    /// <returns></returns>
    private bool Equals(ISmartEnum other)
    {
        return Description == other.Description && Name == other.Name;
    }

    /// <inheritdoc />
    ISmartEnum? ISmartEnum.FindByName(string? name)
    {
        return FindByName(name);
    }
}
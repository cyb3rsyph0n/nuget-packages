using System.ComponentModel;
using System.Text.Json.Serialization;
using TypedGuids.TypeConverters;

namespace TypedGuids;

/// <summary>
///     Type specific guid
/// </summary>
/// <typeparam name="TType">Required type this guid is specific to</typeparam>
[TypeConverter(typeof(TypedGuidTypeConverter))]
[JsonConverter(typeof(TypedGuidJsonConverterFactory))]
public class TypedGuid<TType>
{
    private readonly Guid id;

    /// <summary>
    ///     Default ctor for serialization or db generation
    /// </summary>
    public TypedGuid()
    {
    }

    /// <summary>
    ///     Create a typed guid from a guid string
    /// </summary>
    /// <param name="id">Required id to be parse</param>
    public TypedGuid(string id)
    {
        if (!Guid.TryParse(id, out var guid)) throw new ArgumentException("Invalid guid", nameof(id));
        this.id = guid;
    }

    /// <summary>
    ///     Create a typed guid from a guid
    /// </summary>
    /// <param name="id">Required id to populate internal id with</param>
    public TypedGuid(Guid id)
    {
        this.id = id;
    }

    /// <summary>
    ///     Underlying guid id
    /// </summary>
    private Guid Id => id;

    /// <summary>
    ///     Class that is being typed for
    /// </summary>
    public virtual Type TypedFor => typeof(TType);

    /// <summary>
    ///     Return the actual guid io
    /// </summary>
    public virtual Guid Value => id;

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((TypedGuid<TType>) obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    /// <summary>
    ///     Override for == testing type checked guids
    /// </summary>
    /// <param name="a">Required left operand</param>
    /// <param name="b">Required right operand</param>
    /// <returns></returns>
    public static bool operator ==(TypedGuid<TType> a, TypedGuid<TType> b)
    {
        return a.Id == b.Id;
    }

    /// <summary>
    ///     Override implicit operator for creating a TypedGuid from a string
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static implicit operator TypedGuid<TType>(string s)
    {
        return new TypedGuid<TType>(s);
    }

    /// <summary>
    ///     Override implicit operator for creating a TypedGuid from a guid
    /// </summary>
    /// <param name="g">Required guid to create from</param>
    /// <returns></returns>
    public static implicit operator TypedGuid<TType>(Guid g)
    {
        return new TypedGuid<TType>(g);
    }

    /// <summary>
    ///     Override for != testing type checked guids
    /// </summary>
    /// <param name="a">Required left operand</param>
    /// <param name="b">Required right operand</param>
    /// <returns></returns>
    public static bool operator !=(TypedGuid<TType> a, TypedGuid<TType> b)
    {
        return !(a == b);
    }

    /// <summary>
    ///     Override to string return guid value
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Id.ToString();
    }

    private bool Equals(TypedGuid<TType> other)
    {
        return id.Equals(other.id);
    }
}
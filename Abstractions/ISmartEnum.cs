namespace Abstractions;

/// <summary>
///     Interface for a BaseEnum
/// </summary>
public interface ISmartEnum
{
    /// <summary>
    ///     Description of the enum
    /// </summary>
    string Description { get; }

    /// <summary>
    ///     Name of the enum
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     Lookup method to find by name
    /// </summary>
    /// <param name="name">Required name to lookup</param>
    /// <returns></returns>
    ISmartEnum? FindByName(string? name);

    /// <summary>
    ///     Return the enum as a list of key value pairs
    /// </summary>
    /// <returns></returns>
    Dictionary<string, string> ToDictionary();
}
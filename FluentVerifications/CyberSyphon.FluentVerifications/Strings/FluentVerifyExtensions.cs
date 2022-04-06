namespace CyberSyphon.FluentVerifications.Strings;

/// <summary>
///     Extension methods for string verification
/// </summary>
public static class FluentVerifyExtensions
{
    /// <summary>
    ///     Adds fluent verifier to string
    /// </summary>
    /// <param name="value">Required string to add to</param>
    /// <returns>FluentStringVerifier</returns>
    public static FluentStringVerifier Verify(this string? value)
    {
        return new FluentStringVerifier(value);
    }
}
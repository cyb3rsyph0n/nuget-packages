namespace CyberSyphon.FluentVerifications.Guids;

/// <summary>
///     Extension methods for guid verification
/// </summary>
public static class FluentVerifyExtensions
{
    /// <summary>
    ///     Adds fluent verifier to nullable guid
    /// </summary>
    /// <param name="value">Required nullable guid to add to</param>
    /// <returns>FluentGuidVerifier</returns>
    public static FluentGuidVerifier Verify(this Guid? value)
    {
        return new FluentGuidVerifier(value);
    }

    /// <summary>
    ///     Adds fluent verifier to guid
    /// </summary>
    /// <param name="value">Required guid to add to</param>
    /// <returns>FluentGuidVerifier</returns>
    public static FluentGuidVerifier Verify(this Guid value)
    {
        return new FluentGuidVerifier(value);
    }
}
using CyberSyphon.FluentVerifications.Numbers.Decimals;
using CyberSyphon.FluentVerifications.Numbers.Integers;

namespace CyberSyphon.FluentVerifications.Numbers;

/// <summary>
///     Fluent verify extension methods
/// </summary>
public static class FluentVerifyExtensions
{
    /// <summary>
    ///     Extends nullable decimals with fluent verification methods.
    /// </summary>
    /// <param name="value">Required value to verify</param>
    /// <returns></returns>
    public static FluentDecimalVerifier Verify(this decimal? value)
    {
        return new FluentDecimalVerifier(value);
    }

    /// <summary>
    ///     Extends decimals with fluent verification methods.
    /// </summary>
    /// <param name="value">Required value to verify</param>
    /// <returns></returns>
    public static FluentDecimalVerifier Verify(this decimal value)
    {
        return new FluentDecimalVerifier(value);
    }

    /// <summary>
    ///     Extends nullable ints with fluent verification methods.
    /// </summary>
    /// <param name="value">Required value to verify</param>
    /// <returns></returns>
    public static FluentIntVerifier Verify(this int? value)
    {
        return new FluentIntVerifier(value);
    }

    /// <summary>
    ///     Extends ints with fluent verification methods.
    /// </summary>
    /// <param name="value">Required value to verify</param>
    /// <returns></returns>
    public static FluentIntVerifier Verify(this int value)
    {
        return new FluentIntVerifier(value);
    }
}
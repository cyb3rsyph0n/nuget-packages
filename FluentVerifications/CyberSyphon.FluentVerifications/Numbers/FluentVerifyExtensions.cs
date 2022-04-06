using CyberSyphon.FluentVerifications.Numbers.Decimals;
using CyberSyphon.FluentVerifications.Numbers.Integers;

namespace CyberSyphon.FluentVerifications.Numbers;

public static class FluentVerifyExtensions
{
    public static FluentDecimalVerifier Verify(this decimal? value)
    {
        return new FluentDecimalVerifier(value);
    }

    public static FluentDecimalVerifier Verify(this decimal value)
    {
        return new FluentDecimalVerifier(value);
    }

    public static FluentIntVerifier Verify(this int? value)
    {
        return new FluentIntVerifier(value);
    }

    public static FluentIntVerifier Verify(this int value)
    {
        return new FluentIntVerifier(value);
    }
}
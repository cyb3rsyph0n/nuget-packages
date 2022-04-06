using CyberSyphon.FluentVerifications.Numbers.Decimals;

namespace CyberSyphon.FluentVerifications.Numbers.Integers;

/// <summary>
///     Fluent int Verifier
/// </summary>
public class FluentIntVerifier : FluentDecimalVerifier
{
    /// <summary>
    ///     Internal constructor
    /// </summary>
    /// <param name="value">Required value being verified</param>
    internal FluentIntVerifier(int? value)
        : base(value)
    {
    }

    /// <summary>
    ///     Override to convert internal decimal value to integer
    /// </summary>
    public new int? Value => (int?) base.Value;

    /// <inheritdoc />
    public override FluentIntVerifier Between(decimal lowerBound, decimal upperBound)
    {
        return (FluentIntVerifier) base.Between(lowerBound, upperBound);
    }

    /// <inheritdoc />
    public override FluentIntVerifier EqualTo(decimal value)
    {
        return (FluentIntVerifier) base.EqualTo(value);
    }

    /// <inheritdoc />
    public override FluentIntVerifier EqualToZero()
    {
        return (FluentIntVerifier) base.EqualToZero();
    }

    /// <inheritdoc />
    public override FluentIntVerifier GreaterThan(decimal value)
    {
        return (FluentIntVerifier) base.GreaterThan(value);
    }

    /// <inheritdoc />
    public override FluentIntVerifier GreaterThanOrEqualTo(decimal value)
    {
        return (FluentIntVerifier) base.GreaterThanOrEqualTo(value);
    }

    /// <inheritdoc />
    public override FluentIntVerifier GreaterThanOrEqualToZero()
    {
        return (FluentIntVerifier) base.GreaterThanOrEqualToZero();
    }

    /// <inheritdoc />
    public override FluentIntVerifier GreaterThanZero()
    {
        return (FluentIntVerifier) base.GreaterThanZero();
    }

    /// <inheritdoc />
    public override FluentIntVerifier LessThan(decimal value)
    {
        return (FluentIntVerifier) base.LessThan(value);
    }

    /// <inheritdoc />
    public override FluentIntVerifier LessThanOrEqualTo(decimal value)
    {
        return (FluentIntVerifier) base.LessThanOrEqualTo(value);
    }

    /// <inheritdoc />
    public override FluentIntVerifier LessThanOrEqualToZero()
    {
        return (FluentIntVerifier) base.LessThanOrEqualToZero();
    }

    /// <inheritdoc />
    public override FluentIntVerifier LessThanZero()
    {
        return (FluentIntVerifier) base.LessThanZero();
    }

    /// <inheritdoc />
    public override FluentIntVerifier Negative()
    {
        return (FluentIntVerifier) base.Negative();
    }

    /// <inheritdoc />
    public override FluentIntVerifier NonZero()
    {
        return (FluentIntVerifier) base.NonZero();
    }

    /// <inheritdoc />
    public override FluentIntVerifier NotBetween(decimal lowerBound, decimal upperBound)
    {
        return (FluentIntVerifier) base.NotBetween(lowerBound, upperBound);
    }

    /// <inheritdoc />
    public override FluentIntVerifier NotEqualTo(decimal value)
    {
        return (FluentIntVerifier) base.NotEqualTo(value);
    }

    /// <inheritdoc />
    public override FluentIntVerifier NotEqualToZero()
    {
        return (FluentIntVerifier) base.NotEqualToZero();
    }

    /// <inheritdoc />
    public override FluentIntVerifier Positive()
    {
        return (FluentIntVerifier) base.Positive();
    }

    /// <inheritdoc />
    public override FluentIntVerifier Zero()
    {
        return (FluentIntVerifier) base.Zero();
    }
}
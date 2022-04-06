namespace CyberSyphon.FluentVerifications.Numbers.Decimals;

/// <summary>
///     Fluent decimal verifier
/// </summary>
public class FluentDecimalVerifier : FluentVerifier<decimal?>
{
    /// <summary>
    ///     Internal constructor
    /// </summary>
    /// <param name="value">Required value being verified</param>
    internal FluentDecimalVerifier(decimal? value)
        : base(value)
    {
    }

    /// <summary>
    ///     Verify between two values
    /// </summary>
    /// <param name="lowerBound">Required lower bound inclusive</param>
    /// <param name="upperBound">Required upper bound inclusive</param>
    /// <returns></returns>
    public virtual FluentDecimalVerifier Between(decimal lowerBound, decimal upperBound)
    {
        Rules.Add(
            new FluentVerifyRule<decimal?>(
                v => v >= lowerBound && v <= upperBound,
                "must be between " + lowerBound + " and " + upperBound
            )
        );
        return this;
    }

    /// <summary>
    ///     Verify equal to a value
    /// </summary>
    /// <param name="value">Required value to test against</param>
    /// <returns></returns>
    public virtual FluentDecimalVerifier EqualTo(decimal value)
    {
        Rules.Add(new FluentVerifyRule<decimal?>(v => v == value, "must be equal to " + value));
        return this;
    }

    /// <summary>
    ///     Verify equal to zero
    /// </summary>
    /// <returns></returns>
    public virtual FluentDecimalVerifier EqualToZero()
    {
        Rules.Add(new FluentVerifyRule<decimal?>(v => v == 0, "must be equal to zero"));
        return this;
    }

    /// <summary>
    ///     Verify greater than a value
    /// </summary>
    /// <param name="value">Required value exclusive</param>
    /// <returns></returns>
    public virtual FluentDecimalVerifier GreaterThan(decimal value)
    {
        Rules.Add(new FluentVerifyRule<decimal?>(v => v > value, "must be greater than " + value));
        return this;
    }

    /// <summary>
    ///     Verify greater than or equal to a value
    /// </summary>
    /// <param name="value">Required value inclusive</param>
    /// <returns></returns>
    public virtual FluentDecimalVerifier GreaterThanOrEqualTo(decimal value)
    {
        Rules.Add(new FluentVerifyRule<decimal?>(v => v >= value, "must be greater than or equal to " + value));
        return this;
    }

    /// <summary>
    ///     Verify greater than or equal to zero
    /// </summary>
    /// <returns></returns>
    public virtual FluentDecimalVerifier GreaterThanOrEqualToZero()
    {
        Rules.Add(new FluentVerifyRule<decimal?>(v => v >= 0, "must be greater than or equal to zero"));
        return this;
    }

    /// <summary>
    ///     Verify greater than zero exclusive
    /// </summary>
    /// <returns></returns>
    public virtual FluentDecimalVerifier GreaterThanZero()
    {
        Rules.Add(new FluentVerifyRule<decimal?>(v => v > 0, "must be greater than zero"));
        return this;
    }

    /// <summary>
    ///     Verify less than a value
    /// </summary>
    /// <param name="value">Required value exclusive</param>
    /// <returns></returns>
    public virtual FluentDecimalVerifier LessThan(decimal value)
    {
        Rules.Add(new FluentVerifyRule<decimal?>(v => v < value, "must be less than " + value));
        return this;
    }

    /// <summary>
    ///     Verify less than or equal to a value
    /// </summary>
    /// <param name="value">Required value inclusive</param>
    /// <returns></returns>
    public virtual FluentDecimalVerifier LessThanOrEqualTo(decimal value)
    {
        Rules.Add(new FluentVerifyRule<decimal?>(v => v <= value, "must be less than or equal to " + value));
        return this;
    }

    /// <summary>
    ///     Verify less than or equal to zero
    /// </summary>
    /// <returns></returns>
    public virtual FluentDecimalVerifier LessThanOrEqualToZero()
    {
        Rules.Add(new FluentVerifyRule<decimal?>(v => v <= 0, "must be less than or equal to zero"));
        return this;
    }

    /// <summary>
    ///     Verify less than zero exclusive
    /// </summary>
    /// <returns></returns>
    public virtual FluentDecimalVerifier LessThanZero()
    {
        Rules.Add(new FluentVerifyRule<decimal?>(v => v < 0, "must be less than zero"));
        return this;
    }

    /// <summary>
    ///     Verify less than zero exclusive
    /// </summary>
    /// <returns></returns>
    public virtual FluentDecimalVerifier Negative()
    {
        Rules.Add(new FluentVerifyRule<decimal?>(v => v < 0, "must be negative"));
        return this;
    }

    /// <summary>
    ///     Verify not zero
    /// </summary>
    /// <returns></returns>
    public virtual FluentDecimalVerifier NonZero()
    {
        Rules.Add(new FluentVerifyRule<decimal?>(v => v != 0, "must not be zero"));
        return this;
    }

    /// <summary>
    ///     Verify is not between
    /// </summary>
    /// <param name="lowerBound">Required lower bound exclusive</param>
    /// <param name="upperBound">Required lower bound inclusive</param>
    /// <returns></returns>
    public virtual FluentDecimalVerifier NotBetween(decimal lowerBound, decimal upperBound)
    {
        Rules.Add(
            new FluentVerifyRule<decimal?>(
                v => v < lowerBound || v > upperBound,
                "must not be between " + lowerBound + " and " + upperBound
            )
        );
        return this;
    }

    /// <summary>
    ///     Verify is not equal to
    /// </summary>
    /// <param name="value">Required value exclusive</param>
    /// <returns></returns>
    public virtual FluentDecimalVerifier NotEqualTo(decimal value)
    {
        Rules.Add(new FluentVerifyRule<decimal?>(v => v != value, "must not be equal to " + value));
        return this;
    }

    /// <summary>
    ///     Verify not equal to zero
    /// </summary>
    /// <returns></returns>
    public virtual FluentDecimalVerifier NotEqualToZero()
    {
        Rules.Add(new FluentVerifyRule<decimal?>(v => v != 0, "must not be equal to zero"));
        return this;
    }

    /// <summary>
    ///     Verify greater than zero exclusive
    /// </summary>
    /// <returns></returns>
    public virtual FluentDecimalVerifier Positive()
    {
        Rules.Add(new FluentVerifyRule<decimal?>(v => v > 0, "must be positive"));
        return this;
    }

    /// <summary>
    ///     Verify is zero
    /// </summary>
    /// <returns></returns>
    public virtual FluentDecimalVerifier Zero()
    {
        Rules.Add(new FluentVerifyRule<decimal?>(v => v == 0, "must be zero"));
        return this;
    }
}
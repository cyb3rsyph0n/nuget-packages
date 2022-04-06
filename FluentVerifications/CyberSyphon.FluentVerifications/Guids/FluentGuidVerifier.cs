namespace CyberSyphon.FluentVerifications.Guids;

/// <summary>
///     Fluent guid verifier
/// </summary>
public class FluentGuidVerifier : FluentVerifier<Guid?>
{
    /// <inheritdoc />
    public FluentGuidVerifier(Guid? value)
        : base(value)
    {
    }

    /// <summary>
    ///     Verifies that the value is Guid.Empty
    /// </summary>
    /// <returns>FluentGuidVerifier</returns>
    public FluentGuidVerifier IsEmpty()
    {
        Rules.Add(new FluentVerifyRule<Guid?>(v => v.HasValue && v.Value == Guid.Empty, "cannot be empty"));
        return this;
    }

    /// <summary>
    ///     Verifies that the value is not null && not Guid.Empty
    /// </summary>
    /// <returns>FluentGuidVerifier</returns>
    public FluentGuidVerifier IsNotEmpty()
    {
        Rules.Add(new FluentVerifyRule<Guid?>(v => v.HasValue && v.Value != Guid.Empty, "cannot be empty"));
        return this;
    }
}
using CyberSyphon.FluentVerifications.Exceptions;

namespace CyberSyphon.FluentVerifications;

/// <summary>
///     Base fluent verifier
/// </summary>
/// <typeparam name="TType">Required type being verified</typeparam>
public abstract class FluentVerifier<TType>
{
    protected readonly IList<FluentVerifyRule<TType>> Rules = new List<FluentVerifyRule<TType>>();
    private readonly TType? verifyValue;

    /// <summary>
    ///     Default ctor
    /// </summary>
    /// <param name="value">Required value being verified</param>
    protected FluentVerifier(TType? value)
    {
        verifyValue = value;
    }

    /// <summary>
    ///     If set this will be the exception type thrown when a rule fails
    /// </summary>
    private Type ExceptionType { get; set; } = null!;

    /// <summary>
    ///     Name of the value for messages
    /// </summary>
    private string? NameOverride { get; set; }

    /// <summary>
    ///     Separator to use when joining the errors
    /// </summary>
    private string Separator { get; set; } = ", ";

    /// <summary>
    ///     Value that is being verified
    /// </summary>
    public TType? Value => verifyValue;

    /// <summary>
    ///     Add custom rule to the verifier
    /// </summary>
    /// <param name="rule">Required custom test method</param>
    /// <param name="message">Required custom message for the rule</param>
    public FluentVerifier<TType> CustomRule(Func<TType?, bool> rule, string message)
    {
        Rules.Add(new FluentVerifyRule<TType>(rule, message));
        return this;
    }

    /// <summary>
    ///     Add custom rule to the verifier
    /// </summary>
    /// <param name="rule">Required custom rule</param>
    public FluentVerifier<TType> CustomRule(FluentVerifyRule<TType> rule)
    {
        Rules.Add(rule);
        return this;
    }

    /// <summary>
    ///     Return true if there are verification errors found
    /// </summary>
    public bool HasErrors()
    {
        return !Success();
    }

    /// <summary>
    ///     Verify the value is equal to the required value
    /// </summary>
    /// <param name="value">Required value to test against</param>
    public FluentVerifier<TType> IsEqualTo(TType value)
    {
        Rules.Add(
            new FluentVerifyRule<TType>(v => v == null ? value == null : v.Equals(value), $"must be equal to '{value}'")
        );
        return this;
    }

    /// <summary>
    ///     Verify value is not null
    /// </summary>
    public FluentVerifier<TType> IsNotNull()
    {
        Rules.Add(new FluentVerifyRule<TType>(v => v != null, "cannot be null"));
        return this;
    }

    /// <summary>
    ///     Verify value is null
    /// </summary>
    public FluentVerifier<TType> IsNull()
    {
        Rules.Add(new FluentVerifyRule<TType>(v => v == null, "must be null"));
        return this;
    }

    /// <summary>
    ///     Run the verification
    /// </summary>
    /// <returns>String containing all the errors separated by commas</returns>
    public string Error()
    {
        string GetResultString() => string.Join(Separator, Rules.Select(rule => rule.GetResult(verifyValue)));

        if (string.IsNullOrEmpty(NameOverride))
        {
            return GetResultString();
        }

        foreach (var rule in Rules)
            rule.SetName(NameOverride);

        return GetResultString();
    }

    /// <summary>
    ///     Return true if the verification was successful
    /// </summary>
    public bool Success()
    {
        return Error() == "";
    }

    /// <summary>
    ///     Throws exception if there are verification errors found
    /// </summary>
    public void ThrowIfHasErrors()
    {
        var error = Error();

        if (string.IsNullOrEmpty(error)) return;

        if (ExceptionType == null)
            throw new FluentVerificationException(error);

        throw (Exception) Activator.CreateInstance(ExceptionType, error)!;
    }

    /// <summary>
    ///     Override the exception type to throw
    /// </summary>
    /// <param name="type">Required type to throw</param>
    public FluentVerifier<TType> WithException(Type type)
    {
        ExceptionType = type ?? throw new ArgumentNullException(nameof(type));
        return this;
    }

    /// <summary>
    ///     Overrides the message for the last rule added
    /// </summary>
    /// <param name="message"></param>
    public FluentVerifier<TType> WithMessage(string message)
    {
        if (Rules.Count == 0)
            throw new InvalidOperationException("No rules have been added");

        if (string.IsNullOrEmpty(message))
            throw new ArgumentException("Message cannot be null or empty", nameof(message));

        Rules.Last().SetMessage(message);
        return this;
    }

    /// <summary>
    ///     Change the separator used in the result
    /// </summary>
    /// <param name="separator"></param>
    public FluentVerifier<TType> WithSeparator(string separator)
    {
        Separator = separator ?? throw new ArgumentNullException(nameof(separator));
        return this;
    }

    /// <summary>
    ///     Update the name being used for the parameter
    /// </summary>
    /// <param name="name"></param>
    protected FluentVerifier<TType> WithName(string name)
    {
        NameOverride = name ?? throw new ArgumentNullException(nameof(name));
        return this;
    }
}
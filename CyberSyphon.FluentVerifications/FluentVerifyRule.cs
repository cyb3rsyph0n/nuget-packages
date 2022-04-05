namespace CyberSyphon.FluentVerifications;

/// <summary>
///     Generic Fluent Verifier rule
/// </summary>
/// <typeparam name="TType"></typeparam>
public class FluentVerifyRule<TType>
{
    private string message;
    private bool messageOverride;

    /// <summary>
    ///     Default ctor
    /// </summary>
    /// <param name="verifyFunc">Required method to execute for verification</param>
    /// <param name="message">Required message to return if check fails</param>
    public FluentVerifyRule(Func<TType?, bool> verifyFunc, string message)
    {
        VerifyFunc = verifyFunc;
        this.message = message;
    }

    /// <summary>
    ///     Message if check fails
    /// </summary>
    private string Message => messageOverride ? message : $"{Name} {message}";

    /// <summary>
    ///     Name to use in message
    /// </summary>
    private string Name { get; set; } = "Value";

    /// <summary>
    ///     Method to execute to verify
    /// </summary>
    private Func<TType?, bool> VerifyFunc { get; }

    /// <summary>
    ///     Returns the result of the verification
    /// </summary>
    /// <param name="value">Required value to verify</param>
    public string? GetResult(TType? value)
    {
        return VerifyFunc(value) ? null : Message;
    }

    /// <summary>
    ///     Change the message to use if the check fails
    /// </summary>
    /// <param name="newMessage">Required new message</param>
    public void SetMessage(string newMessage)
    {
        messageOverride = true;
        message = newMessage;
    }

    /// <summary>
    ///     Change the name of the value to verify
    /// </summary>
    /// <param name="name">Required new name</param>
    public void SetName(string name)
    {
        Name = name;
    }
}
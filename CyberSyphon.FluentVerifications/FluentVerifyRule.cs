namespace CyberSyphon.FluentVerifications;

public class FluentVerifyRule<TType>
{
    private readonly Func<TType?, bool> verifyMethod;
    private string message = null!;
    private bool messageOverride;
    private string? name;

    /// <summary>
    ///     Default ctor
    /// </summary>
    /// <param name="func">Required method to execute for verification</param>
    /// <param name="message">Required message to return if check fails</param>
    public FluentVerifyRule(Func<TType?, bool> func, string message)
    {
        verifyMethod = func;
        Message = message;
    }

    /// <summary>
    ///     Message if check fails
    /// </summary>
    private string Message
    {
        get => messageOverride ? message : $"{Name} {message}";
        set => message = value;
    }

    /// <summary>
    ///     Name to use in message
    /// </summary>
    private string? Name
    {
        get => $"{name ?? "Value"}";
        set => name = value;
    }

    /// <summary>
    ///     Method to execute to verify
    /// </summary>
    private Func<TType?, bool> VerifyMethod => verifyMethod;

    /// <summary>
    ///     Returns the result of the verification
    /// </summary>
    /// <param name="value">Required value to verify</param>
    public string? GetResult(TType? value)
    {
        return VerifyMethod(value) ? null : Message;
    }

    /// <summary>
    ///     Change the message to use if the check fails
    /// </summary>
    /// <param name="newMessage">Required new message</param>
    public void SetMessage(string newMessage)
    {
        messageOverride = true;
        Message = newMessage;
    }

    /// <summary>
    ///     Change the name of the value to verify
    /// </summary>
    /// <param name="newName">Required new name</param>
    public void SetName(string newName)
    {
        Name = newName;
    }
}
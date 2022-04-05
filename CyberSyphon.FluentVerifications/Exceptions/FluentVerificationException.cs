namespace CyberSyphon.FluentVerifications.Exceptions;

/// <summary>
///     Default exception for verification exceptions
/// </summary>
public class FluentVerificationException : Exception
{
    /// <inheritdoc />
    public FluentVerificationException(string message)
        : base(message)
    {
    }
}
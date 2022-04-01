using System.Diagnostics.CodeAnalysis;
using CyberSyphon.Abstractions;

namespace CyberSyphon.Security;

/// <summary>
///     NoOp encryption provider to not actually encrypt data
/// </summary>
[ExcludeFromCodeCoverage]
public class NoOpEncryptionProvider : IEncryptionProvider
{
    /// <inheritdoc />
    public string Decrypt(string base64EncodedString, string password)
    {
        return base64EncodedString;
    }

    /// <inheritdoc />
    public string Encrypt(string value, string password)
    {
        return value;
    }

    /// <inheritdoc />
    public string GenerateSaltedHash(string valueToEncrypt, string salt)
    {
        return valueToEncrypt;
    }

    /// <inheritdoc />
    public bool VerifySaltedHash(string value, string believedValue, string salt)
    {
        return value == believedValue;
    }
}
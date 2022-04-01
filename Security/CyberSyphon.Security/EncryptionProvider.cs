using CyberSyphon.Abstractions;
using CyberSyphon.Security.Extensions;

namespace CyberSyphon.Security;

/// <summary>
///     Encryption provider with app settings injected
/// </summary>
public class EncryptionProvider : IEncryptionProvider, ITransientInjection
{
    /// <inheritdoc />
    public string Decrypt(string base64EncodedString, string password)
    {
        return base64EncodedString.Decrypt(password);
    }

    /// <inheritdoc />
    public string Encrypt(string value, string password)
    {
        return value.Encrypt(password);
    }

    /// <inheritdoc />
    public string GenerateSaltedHash(string valueToEncrypt, string salt)
    {
        return valueToEncrypt.GenerateSaltedHash(salt);
    }

    /// <inheritdoc />
    public bool VerifySaltedHash(string value, string believedValue, string salt)
    {
        return value.VerifySaltedHash(believedValue, salt);
    }
}
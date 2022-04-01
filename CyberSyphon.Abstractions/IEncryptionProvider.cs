namespace CyberSyphon.Abstractions;

/// <summary>
///     Encryption provider
/// </summary>
public interface IEncryptionProvider
{
    /// <summary>
    ///     Decrypt a string using the provided password
    /// </summary>
    /// <param name="base64EncodedString">Required Base64 encoded encrypted value to decrypt</param>
    /// <param name="password">Required password that was used during encryption</param>
    string Decrypt(string base64EncodedString, string password);

    /// <summary>
    ///     Encrypt a string using the provided password
    /// </summary>
    /// <param name="value">Required value to encrypt</param>
    /// <param name="password">Required password to use during encryption</param>
    /// <returns>Base64 encoded string representation of encrypted value</returns>
    string Encrypt(string value, string password);

    /// <summary>
    ///     Generates a salted hash for the given string
    /// </summary>
    /// <param name="valueToEncrypt">Required value to hash</param>
    /// <param name="salt">Required salt to perform hash with</param>
    string GenerateSaltedHash(string valueToEncrypt, string salt);

    /// <summary>
    ///     Verify the salted hash matches the believed value
    /// </summary>
    /// <param name="value">Required value to check</param>
    /// <param name="believedValue">Required believed value to compare against</param>
    /// <param name="salt">Required salt used to create hash</param>
    bool VerifySaltedHash(string value, string believedValue, string salt);
}
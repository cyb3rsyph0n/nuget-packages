using System.Security.Cryptography;
using System.Text;

namespace CyberSyphon.Security.Extensions;

/// <summary>
///     Extension methods for strings
/// </summary>
public static class StringExtensions
{
    /// <summary>
    ///     All Printable character constants
    /// </summary>
    private const string PrintableChars =
        @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890-=`~!@#$%^&*()_+,./;'[]\{}|:""<>?";

    /// <summary>
    ///     Compute SHA256 hash from string value
    /// </summary>
    /// <param name="s">Required string</param>
    public static string ComputeSha256Hash(this string s)
    {
        using var sha256Hash = SHA256.Create();
        var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(s));

        var builder = new StringBuilder();
        foreach (var t in bytes)
            builder.Append(t.ToString("x2"));

        return builder.ToString();
    }

    /// <summary>
    ///     Decrypt encrypted base64 string
    /// </summary>
    /// <param name="s">Required base64 encrypted string</param>
    /// <param name="password">Optional password</param>
    public static string Decrypt(this string s, string password)
    {
        var passBytes = Encoding.UTF8.GetBytes(password);
        var encBytes = new byte[]
        {
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };
        var len = passBytes.Length;

        if (len > encBytes.Length) len = encBytes.Length;

        Array.Copy(passBytes, encBytes, len);

        var aesManaged = Aes.Create();
        aesManaged.KeySize = 256;
        aesManaged.Mode = CipherMode.CBC;
        aesManaged.Padding = PaddingMode.PKCS7;
        aesManaged.Key = encBytes;
        aesManaged.IV = encBytes;

        var decryptor = aesManaged.CreateDecryptor();
        var endData = Convert.FromBase64String(s);

        return Encoding.UTF8.GetString(decryptor.TransformFinalBlock(endData, 0, endData.Length));
    }

    /// <summary>
    ///     Encrypt a string using Aes
    /// </summary>
    /// <param name="s">Required string to encrypt</param>
    /// <param name="password">Optional password</param>
    public static string Encrypt(this string s, string password)
    {
        var passBytes = Encoding.UTF8.GetBytes(password);
        var encBytes = new byte[]
        {
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };
        var len = passBytes.Length;

        if (len > encBytes.Length) len = encBytes.Length;

        Array.Copy(passBytes, encBytes, len);

        var aesManaged = Aes.Create();
        aesManaged.KeySize = 256;
        aesManaged.Mode = CipherMode.CBC;
        aesManaged.Padding = PaddingMode.PKCS7;
        aesManaged.Key = encBytes;
        aesManaged.IV = encBytes;


        var encryptor = aesManaged.CreateEncryptor();
        var textData = Encoding.UTF8.GetBytes(s);

        return Convert.ToBase64String(encryptor.TransformFinalBlock(textData, 0, textData.Length));
    }

    /// <summary>
    ///     Generate a random string from a list of valid characters
    /// </summary>
    /// <param name="stringLength">Required length of string to generate</param>
    /// <param name="validCharacters">Required list of valid characters to choose from</param>
    public static string GenerateRandomString(int stringLength, string validCharacters = PrintableChars)
    {
        var validChars = validCharacters.ToCharArray();
        var maxValue = validChars.Length;
        var code = new StringBuilder();

        for (var i = 0; i < stringLength; i++) code.Append(validChars[RandomNumberGenerator.GetInt32(0, maxValue)]);

        return code.ToString();
    }

    /// <summary>
    ///     Generate a salted hash of the incoming string
    /// </summary>
    /// <param name="s">string to salt and hash</param>
    /// <param name="knownSalt">Required known salt value to add to string</param>
    /// <param name="randSaltLength">number of bytes to use for salt</param>
    /// <returns>returns a byte array of the salted hash</returns>
    public static string GenerateSaltedHash(this string s, string knownSalt, int randSaltLength = 8)
    {
        var randomSalt = GenerateRandomString(randSaltLength);
        var randomSaltBytes = Encoding.Default.GetBytes(randomSalt);

        var computedHash = SHA256.Create().ComputeHash(Encoding.Default.GetBytes(randomSalt + s + knownSalt));
        var result = new byte[computedHash.Length + randomSalt.Length];

        Array.Copy(computedHash, result, computedHash.Length);
        Array.Copy(randomSaltBytes, 0, result, computedHash.Length, randomSaltBytes.Length);

        return Convert.ToBase64String(result);
    }

    /// <summary>
    ///     Verifies that a salted hash matches the incoming value
    /// </summary>
    /// <param name="s">Base64 encoded string to compare to</param>
    /// <param name="believedValue">this is the believed value</param>
    /// <param name="knownSalt">Required known salt value</param>
    /// <param name="randomSaltLength">number of bytes to use as the salt</param>
    /// <returns>true if the believed value matches the salted hash</returns>
    public static bool VerifySaltedHash(this string s, string believedValue, string knownSalt, int randomSaltLength = 8)
    {
        var actualBytes = Convert.FromBase64String(s);
        var randomSaltBytes = actualBytes.TakeLast(randomSaltLength).ToArray();
        var randomSalt = Encoding.Default.GetString(randomSaltBytes);

        var computedHash = SHA256.Create()
            .ComputeHash(Encoding.Default.GetBytes(randomSalt + believedValue + knownSalt));

        return actualBytes.Take(actualBytes.Length - randomSaltLength).SequenceEqual(computedHash);
    }
}
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
    ///     Generate salted hash from string
    /// </summary>
    /// <param name="s">Required string input to be hashed</param>
    /// <param name="knownSalt">Required known salt value</param>
    /// <param name="randSaltLength">Optional number of random salt bytes to be added</param>
    /// <param name="iterations">Optional number of iterations to perform</param>
    /// <param name="hashSize">Optional desired number of hash bytes, will be added to the length of random salt bytes</param>
    /// <returns></returns>
    public static string GenerateSaltedHash(
        this string s,
        string knownSalt,
        int randSaltLength = 32,
        int iterations = 100000,
        int hashSize = 32
    )
    {
        var provider = RandomNumberGenerator.Create();
        var saltBytes = new byte[randSaltLength];
        provider.GetBytes(saltBytes);

        Console.WriteLine(Convert.ToBase64String(saltBytes));

        var pbkdf2 = new Rfc2898DeriveBytes(s + knownSalt, saltBytes, iterations);
        return Convert.ToBase64String(pbkdf2.GetBytes(hashSize).Concat(saltBytes).ToArray());
    }

    /// <summary>
    ///     Verify a salted hash
    /// </summary>
    /// <param name="s">Required base64 encoded hash value</param>
    /// <param name="believedValue">Required believed value of the string</param>
    /// <param name="knownSalt">Required known salt value</param>
    /// <param name="randSaltLength">Optional how many bytes of random salt to be added to the hash</param>
    /// <param name="iterations">Optional number of iterations to perform during hash</param>
    /// <param name="hashSize">Required desired number of hash bytes, this will be added to the length of random salt bytes</param>
    /// <returns></returns>
    public static bool VerifySaltedHash(
        this string s,
        string believedValue,
        string knownSalt,
        int randSaltLength = 32,
        int iterations = 100000,
        int hashSize = 32
    )
    {
        var bytes = Convert.FromBase64String(s);
        var rndSalt = bytes.Skip(bytes.Length - randSaltLength).ToArray();

        Console.WriteLine(Convert.ToBase64String(rndSalt));

        var pbkdf2 = new Rfc2898DeriveBytes(believedValue + knownSalt, rndSalt, iterations);

        return pbkdf2.GetBytes(hashSize).SequenceEqual(bytes.Take(hashSize));
    }
}
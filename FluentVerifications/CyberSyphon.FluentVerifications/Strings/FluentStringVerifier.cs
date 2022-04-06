using System.Text.RegularExpressions;

namespace CyberSyphon.FluentVerifications.Strings;

/// <summary>
///     Fluent string verifier
/// </summary>
public class FluentStringVerifier : FluentVerifier<string>
{
    /// <inheritdoc />
    public FluentStringVerifier(string? value)
        : base(value)
    {
    }

    /// <summary>
    ///     Verify the string is a valid email address.
    /// </summary>
    public FluentStringVerifier IsEmailAddress()
    {
        var regex = new Regex(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$"
        );

        Rules.Add(new FluentVerifyRule<string>(v => !string.IsNullOrEmpty(v) && regex.IsMatch(v), "must be a valid email address"));
        return this;
    }

    /// <summary>
    ///     Verifies that the string is empty
    /// </summary>
    public FluentStringVerifier IsEmpty()
    {
        Rules.Add(new FluentVerifyRule<string>(v => v == string.Empty, "must be empty"));
        return this;
    }

    /// <summary>
    ///     Verifies that the string is not null
    /// </summary>
    public FluentStringVerifier IsNotEmpty()
    {
        Rules.Add(new FluentVerifyRule<string>(v => !string.IsNullOrEmpty(v), "cannot be empty"));
        return this;
    }

    public FluentStringVerifier IsNotNullOrEmpty()
    {
        Rules.Add(new FluentVerifyRule<string>(v => !string.IsNullOrEmpty(v), "cannot be null or empty"));
        return this;
    }

    /// <summary>
    ///     Verifies that the string is the desired length
    /// </summary>
    /// <param name="min">Required min string length</param>
    /// <param name="max">Optional max string length</param>
    public FluentStringVerifier Length(int min, int? max = null)
    {
        Rules.Add(
            max == null
                ? new FluentVerifyRule<string>(
                    v => v != null && v.Length >= min,
                    $"must be at least {min} characters long"
                )
                : new FluentVerifyRule<string>(
                    v => v != null && v.Length >= min && v.Length <= max,
                    $"must be between {min} and {max} characters long"
                )
        );
        return this;
    }

    public new FluentStringVerifier WithName(string name)
    {
        return (base.WithName(name) as FluentStringVerifier)!;
    }
}
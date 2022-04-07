using CyberSyphon.Security.Extensions;
using FluentAssertions;
using Xunit;

namespace Packages.Tests.Security;

public class StringExtensionsTests
{
    [Fact]
    public void VerifySaltedHashExtensionMethods()
    {
        var setup = "Hello, World".GenerateSaltedHash("salt");
        
        setup.Should().NotBe("Hello, World");
        setup.VerifySaltedHash("Hello, World", "salt").Should().BeTrue();
    }
}
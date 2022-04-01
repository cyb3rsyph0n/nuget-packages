using System;
using System.Diagnostics.CodeAnalysis;
using CyberSyphon.TypedGuids;
using FluentAssertions;
using Xunit;

namespace Packages.Tests.TypedGuids;

[ExcludeFromCodeCoverage]
public class TypedGuidTests
{
    [Fact]
    public void VerifyTypedGuidComparesToStrings()
    {
        var newGuid = Guid.NewGuid();
        var setup = new TypedGuid<string>(newGuid);

        (setup == newGuid.ToString()).Should().BeTrue();
    }

    [Fact]
    public void VerifyTypedGuidInequality()
    {
        var setup1 = new TypedGuid<string>(Guid.NewGuid());
        var setup2 = new TypedGuid<string>(Guid.NewGuid());

        (setup1 != setup2).Should().BeTrue();
    }

    [Fact]
    public void VerifyTypedGuidInitializesWithEmptyGuid()
    {
        var setup = new TypedGuid<string>();

        setup.Value.Should().Be(Guid.Empty);
    }

    [Fact]
    public void VerifyTypedGuidToString()
    {
        var newGuid = Guid.NewGuid();
        var setup = new TypedGuid<string>(newGuid);

        setup.ToString().Should().Be(newGuid.ToString());
    }

    [Fact]
    public void VerifyTypedGuidTypedFor()
    {
        var setup = new TypedGuid<string>();

        setup.TypedFor.Should().Be(typeof(string));
    }

    [Fact]
    public void VerifyTypedGuidWithGuidCtor()
    {
        var newGuid = Guid.NewGuid();
        var setup = new TypedGuid<string>(newGuid);

        setup.Should().NotBeNull();
        setup.Value.Should().NotBeEmpty();
        setup.Value.Should().Be(newGuid);
        (setup == newGuid).Should().BeTrue();
    }
}
using System;
using System.Diagnostics.CodeAnalysis;
using CyberSyphon.FluentVerifications.Guids;
using FluentAssertions;
using Xunit;

namespace Packages.Tests.FluentVerifications.Guids;

[ExcludeFromCodeCoverage]
public class FluentGuidVerifierTests
{
    [Fact]
    public void GuidVerifierChecksForNull()
    {
        var setup = Guid.NewGuid();
        setup.Verify().IsNotNull().Success.Should().BeTrue();
    }

    [Fact]
    public void GuidVerifierChecksIsEmpty()
    {
        var setup = Guid.Empty;
        setup.Verify().IsEmpty().Success.Should().BeTrue();
    }

    [Fact]
    public void GuidVerifierChecksIsNotEmpty()
    {
        var setup = Guid.Empty;
        setup.Verify().IsNotEmpty().HasErrors.Should().BeTrue();
    }

    [Fact]
    public void GuidVerifierChecksNullableGuidIsNull()
    {
        Guid? setup = null;
        setup.Verify().IsNull().Success.Should().BeTrue();
    }
}
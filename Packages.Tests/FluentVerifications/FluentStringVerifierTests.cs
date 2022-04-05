using System;
using System.Diagnostics.CodeAnalysis;
using CyberSyphon.FluentVerifications.Exceptions;
using CyberSyphon.FluentVerifications.Strings;
using FluentAssertions;
using Xunit;

namespace Packages.Tests.FluentVerifications;

[ExcludeFromCodeCoverage]
public class FluentStringVerifierTests
{
    [Fact]
    public void StringVerifierChecksIsNull()
    {
        string? setup = null;

        setup.Verify().IsNull().Success().Should().BeTrue();
    }

    [Fact]
    public void StringVerifierChecksIsNullOrEmpty()
    {
        string? setup = null;

        setup.Verify().IsNotNullOrEmpty().HasErrors().Should().BeTrue();
        "".Verify().IsNotNullOrEmpty().HasErrors().Should().BeTrue();
    }

    [Fact]
    public void StringVerifierChecksStringForNull()
    {
        string? setup = null;

        setup.Verify().IsNotNull().Error().Should().Be("Value cannot be null");
    }

    [Theory]
    [InlineData("foo@bar.com")]
    [InlineData("test.123@sample.com")]
    [InlineData("test.12s._3@foobar.sample.co")]
    public void StringVerifierChecksStringIsEmailAddress(string email)
    {
        email.Verify().IsEmailAddress().Success().Should().BeTrue();
    }

    [Fact]
    public void StringVerifierChecksStringIsEmpty()
    {
        string? setup = null;

        setup.Verify().IsEmpty().Error().Should().Be("Value must be empty");
    }

    [Fact]
    public void StringVerifierChecksStringLength()
    {
        "Hello, World!".Verify().Length(4, 13).Success().Should().BeTrue();
        "Hello, World!".Verify().Length(4, 10).Error().Should().Be("Value must be between 4 and 10 characters long");
        "Hel".Verify().Length(4).Error().Should().Be("Value must be at least 4 characters long");
    }

    [Fact]
    public void StringVerifierChecksStringNotEmpty()
    {
        string? setup = null;

        setup.Verify().IsNotEmpty().Error().Should().Be("Value cannot be empty");
    }

    [Fact]
    public void StringVerifierExtendsString()
    {
        "hello, world".Verify().Should().NotBeNull();
    }

    [Fact]
    public void StringVerifierHasErrors()
    {
        var setup = string.Empty;

        setup.Verify().IsEqualTo("Hello, World!").HasErrors().Should().BeTrue();
    }

    [Fact]
    public void StringVerifierIsEqualTo()
    {
        const string setup = "Hello, World!";

        setup.Verify().IsEqualTo("hello, World!").Error().Should().Be("Value must be equal to 'hello, World!'");
    }

    [Fact]
    public void StringVerifierMultipleErrors()
    {
        string? setup = null;

        setup.Verify()
            .WithName("testValue")
            .IsNotNullOrEmpty()
            .Length(5)
            .IsEmailAddress()
            .Error()
            .Should()
            .Be(
                "testValue cannot be null or empty, testValue must be at least 5 characters long, testValue must be a valid email address"
            );
    }

    [Fact]
    public void StringVerifierSuccess()
    {
        const string setup = "Hello, World!";

        setup.Verify().IsEqualTo("Hello, World!").Success().Should().BeTrue();
    }

    [Fact]
    public void StringVerifierThrowsCustomException()
    {
        string? setup = null;
        var act = () => setup.Verify().IsNotNull().WithException(typeof(ArgumentException)).ThrowIfHasErrors();

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void StringVerifierThrowsFluentVerificationException()
    {
        string? setup = null;
        var act = () => setup.Verify().IsNotNull().ThrowIfHasErrors();

        act.Should().Throw<FluentVerificationException>();
    }

    [Fact]
    public void StringVerifierWithMessage()
    {
        string? setup = null;

        setup.Verify().IsEmpty().WithMessage("testing").Error().Should().Be("testing");
    }

    [Fact]
    public void StringVerifierWithName()
    {
        string? setup = null;

        setup.Verify().WithName(nameof(setup)).IsEmpty().Error().Should().Be("setup must be empty");
    }
}
using CyberSyphon.FluentVerifications.Numbers;
using FluentAssertions;
using Xunit;

namespace Packages.Tests.FluentVerifications.Numbers;

public class FluentDecimalVerifierTests
{
    [Fact]
    public void VerifyVerifierMethods()
    {
        const decimal setup = 10m;

        setup.Verify().Value.Should().Be(10m);

        setup.Verify().Between(1, 10).Success.Should().BeTrue();
        setup.Verify().NotBetween(11, 20).Success.Should().BeTrue();

        setup.Verify().EqualTo(10).Success.Should().BeTrue();
        setup.Verify().NotEqualTo(11).Success.Should().BeTrue();

        setup.Verify().EqualToZero().Success.Should().BeFalse();
        setup.Verify().NotEqualToZero().Success.Should().BeTrue();

        setup.Verify().LessThan(10).Success.Should().BeFalse();
        setup.Verify().LessThanZero().Success.Should().BeFalse();
        setup.Verify().LessThanOrEqualTo(9).Success.Should().BeFalse();
        setup.Verify().LessThanOrEqualToZero().Success.Should().BeFalse();

        setup.Verify().GreaterThan(9).Success.Should().BeTrue();
        setup.Verify().GreaterThanZero().Success.Should().BeTrue();
        setup.Verify().GreaterThanOrEqualTo(10).Success.Should().BeTrue();
        setup.Verify().GreaterThanOrEqualToZero().Success.Should().BeTrue();

        setup.Verify().Positive().Success.Should().BeTrue();
        setup.Verify().Negative().Success.Should().BeFalse();

        setup.Verify().NonZero().Success.Should().BeTrue();
        setup.Verify().Zero().Success.Should().BeFalse();
    }
}
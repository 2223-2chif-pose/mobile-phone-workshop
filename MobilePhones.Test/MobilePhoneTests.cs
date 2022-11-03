using FluentAssertions;
using Xunit;

namespace MobilePhones.Test;

public sealed class MobilePhoneTests
{
    [Theory]
    [InlineData("+43664123456")]
    [InlineData("+43699654321")]
    [InlineData("0043664123456")]
    [InlineData("0043650123456789")]
    public void Construction_NumberOnly_Valid(string phoneNumber)
    {
        var mobile = new MobilePhone(phoneNumber);

        mobile.PhoneNumber.Should().Be(phoneNumber);
        mobile.ValidNumber.Should().BeTrue();
    }

    [Theory]
    [InlineData("43664123456", "missing either + or 00 prefix")]
    [InlineData("004366412345", "too short")]
    [InlineData("0043764123456", "operator code has to start with 6")]
    [InlineData("043664123456", "prefix has only one 0")]
    [InlineData("0043688123456", "unknown operator code")]
    [InlineData("+0043650123456", "both + and 00 prefix")]
    [InlineData("+49650123456", "wrong country code")]
    [InlineData("+4965012E456", "contains a non-digit character")]
    [InlineData("", "is empty")]
    public void Construction_NumberOnly_Invalid(string phoneNumber, string reason)
    {
        var mobile = new MobilePhone(phoneNumber);

        mobile.PhoneNumber.Should().Be(phoneNumber);
        mobile.ValidNumber.Should().BeFalse(reason);
    }
}
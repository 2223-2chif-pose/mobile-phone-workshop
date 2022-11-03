using System.Text.RegularExpressions;

namespace MobilePhones;

public sealed class MobilePhone
{
    public string PhoneNumber { get; }
    public bool ValidNumber { get; }

    public MobilePhone(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
        ValidNumber = IsValidPhoneNumber(phoneNumber);
    }

    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return false;
        }

        var regex = new Regex("^(?:\\+|00)43(6\\d{2})\\d{6,}");
        var match = regex.Match(phoneNumber);

        if (!match.Success
            || match.Groups.Count != 2
            || !int.TryParse(match.Groups[1].Value, out var operatorCode))
        {
            return false;
        }

        var allowedOperators = new[] { 664, 650, 699, 681 };
        foreach (var @operator in allowedOperators)
        {
            if (@operator == operatorCode)
            {
                return true;
            }
        }

        return false;
    }
}
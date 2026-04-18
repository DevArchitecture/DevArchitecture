using System.Text.RegularExpressions;

namespace Blazor.Admin.Services;

/// <summary>
/// UI and API use culture codes (e.g. tr-TR, en-US). Legacy localStorage may contain display names like "Türkçe" from older clients.
/// </summary>
public static class UiLanguageCode
{
    private static readonly Regex CultureLike = new(
        @"^[a-zA-Z]{2,3}(-[a-zA-Z0-9]{2,12})+$",
        RegexOptions.CultureInvariant | RegexOptions.Compiled);

    /// <summary>
    /// Invalid tags that match BCP 47 shape but use non-flag region codes (e.g. en-EN: "EN" is not ISO 3166-1).
    /// </summary>
    private static string CanonicalizeCultureCode(string t)
    {
        var segments = t.Split(['-', '_'], StringSplitOptions.RemoveEmptyEntries);
        if (segments.Length >= 2
            && string.Equals(segments[0], "en", StringComparison.OrdinalIgnoreCase)
            && string.Equals(segments[1], "en", StringComparison.OrdinalIgnoreCase))
        {
            return "en-US";
        }

        return t;
    }

    public static string Normalize(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
        {
            return "tr-TR";
        }

        var t = raw.Trim();
        if (CultureLike.IsMatch(t))
        {
            return CanonicalizeCultureCode(t);
        }

        return t switch
        {
            "Türkçe" => "tr-TR",
            "Turkish" => "tr-TR",
            "English" => "en-US",
            "İngilizce" => "en-US",
            "Ingilizce" => "en-US",
            _ => "tr-TR"
        };
    }
}

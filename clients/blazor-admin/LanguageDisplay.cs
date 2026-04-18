using Blazor.Admin.Services;

namespace Blazor.Admin;

/// <summary>
/// Regional indicator flag emoji from BCP 47 locale (e.g. tr-TR → 🇹🇷, en-US → 🇺🇸).
/// </summary>
public static class LanguageDisplay
{
    private const string Globe = "\uD83C\uDF10";

    public static string FlagEmojiForLocale(string locale)
    {
        if (string.IsNullOrWhiteSpace(locale))
        {
            return Globe;
        }

        var trimmed = locale.Trim();
        var parts = trimmed.Split(new[] { '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
        string? region = null;
        if (parts.Length >= 2 && parts[1].Length == 2)
        {
            region = parts[1].ToUpperInvariant();
        }
        else if (parts.Length >= 1 && parts[0].Length == 2)
        {
            region = parts[0].ToUpperInvariant();
        }
        else if (parts.Length >= 1)
        {
            region = RegionFromLanguageCode(parts[0].ToLowerInvariant());
        }

        // "EN" is not assigned in ISO 3166-1; regional-indicator pair renders as tofu, not a flag
        if (string.Equals(region, "EN", StringComparison.Ordinal))
        {
            region = "US";
        }

        if (string.IsNullOrEmpty(region) || region.Length != 2
            || !char.IsAsciiLetter(region[0]) || !char.IsAsciiLetter(region[1]))
        {
            return Globe;
        }

        static string RegionalIndicatorPair(string r) =>
            char.ConvertFromUtf32(0x1F1E6 + char.ToUpperInvariant(r[0]) - 'A')
            + char.ConvertFromUtf32(0x1F1E6 + char.ToUpperInvariant(r[1]) - 'A');

        return RegionalIndicatorPair(region);
    }

    private static string? RegionFromLanguageCode(string lang) => lang switch
    {
        "tr" => "TR",
        "en" => "US",
        "de" => "DE",
        "fr" => "FR",
        "ar" => "SA",
        "ru" => "RU",
        "es" => "ES",
        "it" => "IT",
        "nl" => "NL",
        "pl" => "PL",
        "pt" => "PT",
        "ja" => "JP",
        "ko" => "KR",
        "zh" => "CN",
        _ => null
    };

    public static string LabelForLocale(string code) => code switch
    {
        "tr-TR" => "Türkçe",
        "en-US" => "English (US)",
        "en-GB" => "English (UK)",
        "de-DE" => "Deutsch",
        "fr-FR" => "Français",
        _ => code
    };

    public sealed record LanguageOption(string Code, string Label, string Flag);

    public static List<LanguageOption> ToLanguageOptions(IEnumerable<string> codes) =>
        codes.Select(c =>
        {
            var n = UiLanguageCode.Normalize(c);
            return new LanguageOption(n, LabelForLocale(n), FlagEmojiForLocale(n));
        }).ToList();
}

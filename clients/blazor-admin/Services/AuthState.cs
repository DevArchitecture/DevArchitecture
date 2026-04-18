namespace Blazor.Admin.Services;

public sealed class AuthState
{
    private string? _token;
    private readonly HashSet<string> _claims = [];
    private readonly Dictionary<string, string> _translations = new(StringComparer.OrdinalIgnoreCase);
    private string _lang = "tr-TR";
    public event Action? Changed;

    /// <summary>
    /// Set to true after App startup finishes restoring session from localStorage (or skipping when none).
    /// </summary>
    public bool IsSessionReady { get; private set; }

    public void SetSessionReady()
    {
        IsSessionReady = true;
        Changed?.Invoke();
    }

    public bool IsAuthenticated => !string.IsNullOrWhiteSpace(_token);

    public string? Token => _token;
    public string Lang => _lang;

    public IReadOnlyCollection<string> Claims => _claims;

    public void SetToken(string token, IEnumerable<string>? claims = null)
    {
        _token = token;
        _claims.Clear();
        if (claims is not null)
        {
            foreach (var claim in claims)
            {
                _claims.Add(claim);
            }
        }
        Changed?.Invoke();
    }

    public void Logout()
    {
        _token = null;
        _claims.Clear();
        _translations.Clear();
        Changed?.Invoke();
    }

    public bool HasClaim(string claim)
    {
        if (string.IsNullOrWhiteSpace(claim))
        {
            return true;
        }

        return _claims.Contains(claim);
    }

    public void SetLang(string lang)
    {
        _lang = UiLanguageCode.Normalize(lang);
        Changed?.Invoke();
    }

    public void SetTranslations(Dictionary<string, string> values)
    {
        _translations.Clear();
        foreach (var pair in values)
        {
            foreach (var candidate in GetCandidates(pair.Key))
            {
                _translations[candidate] = pair.Value;
            }
        }
        Changed?.Invoke();
    }

    public string T(string key, string fallback)
    {
        foreach (var candidate in GetCandidates(key))
        {
            if (_translations.TryGetValue(candidate, out var value) && !string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
        }

        foreach (var fallbackCandidate in GetCandidates(fallback))
        {
            if (_translations.TryGetValue(fallbackCandidate, out var fallbackValue) && !string.IsNullOrWhiteSpace(fallbackValue))
            {
                return fallbackValue;
            }
        }

        return fallback;
    }

    private static IEnumerable<string> GetCandidates(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return [string.Empty];
        }

        var normalized = key.Trim().ToLowerInvariant();
        var set = new HashSet<string>
        {
            normalized,
            normalized.Replace(".", "_"),
            normalized.Replace("_", "."),
            normalized.Replace(" ", string.Empty)
        };

        if (normalized.StartsWith("menu."))
        {
            set.Add(normalized[5..]);
        }

        if (normalized.StartsWith("action."))
        {
            set.Add(normalized[7..]);
        }

        return set;
    }
}

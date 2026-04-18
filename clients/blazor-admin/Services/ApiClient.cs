using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blazor.Admin.Services;

public sealed class ApiClient(HttpClient httpClient, AuthState authState, IAuthSessionStorage sessionStorage)
{
    private static readonly JsonSerializerOptions CamelCaseSerialize = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public string BaseUrl => "https://localhost:5101/api/v1";

    public async Task<bool> LoginAsync(string email, string password, string lang)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/Auth/login");
            request.Headers.Add("x-dev-arch-version", "1.0");
            request.Content = JsonContent.Create(new { email, password, lang });

            using var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var payload = await response.Content.ReadFromJsonAsync<LoginResponse>();
            var token = payload?.Data?.Token ?? payload?.Token;
            var claims = payload?.Data?.Claims ?? payload?.Claims ?? [];
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            authState.SetToken(token, claims);
            IReadOnlyList<string> claimList = claims ?? Array.Empty<string>();
            await sessionStorage.SaveAsync(token, claimList, UiLanguageCode.Normalize(lang));
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Language codes from API (same source as login page).
    /// </summary>
    public async Task<IReadOnlyList<string>> GetLanguageCodesAsync()
    {
        try
        {
            var list = await QueryAsync("/languages/codes");
            // API returns SelectionItem: id = culture code (Language.Code), label = display name (Language.Name). Never use label for routes/storage.
            var codes = list
                .Select(static row => GetCultureCodeFromLookupRow(row))
                .Where(static x => !string.IsNullOrWhiteSpace(x))
                .Cast<string>()
                .Select(UiLanguageCode.Normalize)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
            return codes.Count > 0 ? codes : ["tr-TR"];
        }
        catch
        {
            return ["tr-TR"];
        }
    }

    private static string? GetCultureCodeFromLookupRow(Dictionary<string, object?> row)
    {
        foreach (var key in new[] { "code", "Code", "id", "Id" })
        {
            if (row.TryGetValue(key, out var v) && v is not null)
            {
                var s = v.ToString()?.Trim();
                if (!string.IsNullOrWhiteSpace(s))
                {
                    return s;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Switch UI language: updates AuthState, reloads translation dictionary from API, persists devarch.lang when logged in.
    /// </summary>
    public async Task SwitchUiLanguageAsync(string lang)
    {
        if (string.IsNullOrWhiteSpace(lang))
        {
            return;
        }

        authState.SetLang(lang);
        var dictionary = await GetTranslationsAsync(authState.Lang);
        authState.SetTranslations(dictionary);
        if (authState.Token is { Length: > 0 } token)
        {
            await sessionStorage.SaveAsync(token, authState.Claims.ToArray(), authState.Lang);
        }
    }

    public async Task<IReadOnlyList<Dictionary<string, object?>>> GetListAsync(string endpoint)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}{WithCacheBust(endpoint)}");
            request.Headers.Add("x-dev-arch-version", "1.0");
            request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue
            {
                NoCache = true,
                NoStore = true,
                MustRevalidate = true
            };
            request.Headers.Pragma.ParseAdd("no-cache");
            if (authState.Token is { Length: > 0 } token)
            {
                request.Headers.Authorization = new("Bearer", token);
            }

            using var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return [];
            }

            var result = await response.Content.ReadFromJsonAsync<List<Dictionary<string, object?>>>();
            return result ?? [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<IReadOnlyList<Dictionary<string, object?>>> QueryAsync(string endpoint)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}{WithCacheBust(endpoint)}");
            request.Headers.Add("x-dev-arch-version", "1.0");
            request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue
            {
                NoCache = true,
                NoStore = true,
                MustRevalidate = true
            };
            request.Headers.Pragma.ParseAdd("no-cache");
            if (authState.Token is { Length: > 0 } token)
            {
                request.Headers.Authorization = new("Bearer", token);
            }

            using var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return [];
            }

            try
            {
                var list = await response.Content.ReadFromJsonAsync<List<Dictionary<string, object?>>>();
                if (list is not null)
                {
                    return list;
                }
            }
            catch
            {
                // ignored: fallback to single row conversion
            }

            var single = await response.Content.ReadFromJsonAsync<Dictionary<string, object?>>();
            return single is null ? [] : [single];
        }
        catch
        {
            return [];
        }
    }

    public Task<bool> CreateAsync(string endpoint, string jsonPayload) =>
        MutateAsync(HttpMethod.Post, endpoint, jsonPayload);

    public Task<bool> UpdateAsync(string endpoint, string jsonPayload) =>
        MutateAsync(HttpMethod.Put, endpoint, jsonPayload);

    public Task<bool> DeleteAsync(string endpoint, string id) =>
        MutateAsync(HttpMethod.Delete, $"{endpoint}/{id}", null);

    public async Task<Dictionary<string, string>> GetTranslationsAsync(string lang)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/translates/languages/{lang}?_ts={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}");
            request.Headers.Add("x-dev-arch-version", "1.0");
            if (authState.Token is { Length: > 0 } token)
            {
                request.Headers.Authorization = new("Bearer", token);
            }

            using var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return [];
            }

            var raw = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(raw);
            var root = document.RootElement;

            if (root.ValueKind == JsonValueKind.Object
                && root.TryGetProperty("data", out var dataObj)
                && dataObj.ValueKind == JsonValueKind.Object
                && TryGetFlatStringDictionary(dataObj, out var nestedDict)
                && nestedDict.Count > 0)
            {
                return MergeTranslationCandidates(nestedDict);
            }

            var items = ExtractTranslationItems(root).ToList();
            if (items.Count == 0
                && root.ValueKind == JsonValueKind.Object
                && TryGetFlatStringDictionary(root, out var flatDict)
                && flatDict.Count > 0)
            {
                return MergeTranslationCandidates(flatDict);
            }

            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var item in items)
            {
                var code = GetString(item, "code") ?? GetString(item, "key") ?? GetString(item, "name");
                var value = GetString(item, "value") ?? GetString(item, "text") ?? GetString(item, "translation");
                if (!string.IsNullOrWhiteSpace(code) && !string.IsNullOrWhiteSpace(value))
                {
                    foreach (var candidate in GetCandidates(code))
                    {
                        map[candidate] = value;
                    }
                }
            }
            return map;
        }
        catch
        {
            return [];
        }
    }

    private async Task<bool> MutateAsync(HttpMethod method, string endpoint, string? jsonPayload)
    {
        using var request = new HttpRequestMessage(method, $"{BaseUrl}{endpoint}");
        request.Headers.Add("x-dev-arch-version", "1.0");
        if (authState.Token is { Length: > 0 } token)
        {
            request.Headers.Authorization = new("Bearer", token);
        }

        if (jsonPayload is { Length: > 0 })
        {
            var normalized = NormalizeJson(jsonPayload);
            request.Content = new StringContent(normalized, Encoding.UTF8, "application/json");
        }

        using var response = await httpClient.SendAsync(request);
        return response.IsSuccessStatusCode;
    }

    private static string NormalizeJson(string payload)
    {
        try
        {
            var json = JsonSerializer.Deserialize<JsonElement>(payload);
            return JsonSerializer.Serialize(json, CamelCaseSerialize);
        }
        catch
        {
            return "{}";
        }
    }

    private static string WithCacheBust(string endpoint)
    {
        var separator = endpoint.Contains('?') ? '&' : '?';
        return $"{endpoint}{separator}_ts={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
    }

    private static Dictionary<string, string> MergeTranslationCandidates(Dictionary<string, string> source)
    {
        var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var kv in source)
        {
            foreach (var candidate in GetCandidates(kv.Key))
            {
                map[candidate] = kv.Value;
            }
        }
        return map;
    }

    /// <summary>
    /// True when every property value is a JSON string (flat key/value translation map).
    /// </summary>
    private static bool TryGetFlatStringDictionary(JsonElement root, out Dictionary<string, string> map)
    {
        map = new Dictionary<string, string>(StringComparer.Ordinal);
        if (root.ValueKind != JsonValueKind.Object)
        {
            return false;
        }

        foreach (var prop in root.EnumerateObject())
        {
            if (prop.Value.ValueKind != JsonValueKind.String)
            {
                map.Clear();
                return false;
            }

            map[prop.Name] = prop.Value.GetString() ?? "";
        }

        return map.Count > 0;
    }

    private static IEnumerable<JsonElement> ExtractTranslationItems(JsonElement root)
    {
        if (root.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in root.EnumerateArray()) yield return item;
            yield break;
        }

        if (root.ValueKind == JsonValueKind.Object)
        {
            foreach (var propertyName in new[] { "data", "items", "result" })
            {
                if (root.TryGetProperty(propertyName, out var nested) && nested.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in nested.EnumerateArray()) yield return item;
                    yield break;
                }
            }
        }
    }

    private static string? GetString(JsonElement item, string propertyName)
    {
        if (item.ValueKind != JsonValueKind.Object) return null;
        if (!item.TryGetProperty(propertyName, out var value)) return null;
        return value.ValueKind == JsonValueKind.String ? value.GetString() : value.ToString();
    }

    private static IEnumerable<string> GetCandidates(string key)
    {
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

    private sealed record LoginResponse(string? Token, string[]? Claims, LoginData? Data);
    private sealed record LoginData(string Token, string[]? Claims);
}

using System.Text.Json;
using Microsoft.JSInterop;

namespace Blazor.Admin.Services;

/// <summary>
/// Persists auth session in browser localStorage (same keys as angular-admin: devarch.*).
/// </summary>
public sealed class AuthSessionStorage(IJSRuntime js) : IAuthSessionStorage
{
    public const string TokenKey = "devarch.token";
    public const string ClaimsKey = "devarch.claims";
    public const string LangKey = "devarch.lang";

    public async ValueTask SaveAsync(string token, IReadOnlyList<string> claims, string lang)
    {
        try
        {
            await js.InvokeVoidAsync("devArchLocalStorage.setItem", TokenKey, token);
            await js.InvokeVoidAsync("devArchLocalStorage.setItem", ClaimsKey, JsonSerializer.Serialize(claims));
            await js.InvokeVoidAsync("devArchLocalStorage.setItem", LangKey, lang);
        }
        catch (JSException)
        {
            // Ignore when running without browser (tests) or JS unavailable.
        }
    }

    public async ValueTask<StoredSession?> TryLoadAsync()
    {
        try
        {
            var token = await js.InvokeAsync<string?>("devArchLocalStorage.getItem", TokenKey);
            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            var claimsJson = await js.InvokeAsync<string?>("devArchLocalStorage.getItem", ClaimsKey) ?? "[]";
            string[] claims;
            try
            {
                claims = JsonSerializer.Deserialize<string[]>(claimsJson) ?? [];
            }
            catch (JsonException)
            {
                claims = [];
            }

            var rawLang = await js.InvokeAsync<string?>("devArchLocalStorage.getItem", LangKey);
            var lang = UiLanguageCode.Normalize(rawLang);
            if (rawLang is not null
                && !string.Equals(rawLang.Trim(), lang, StringComparison.Ordinal))
            {
                try
                {
                    await js.InvokeVoidAsync("devArchLocalStorage.setItem", LangKey, lang);
                }
                catch (JSException)
                {
                    // ignore
                }
            }

            return new StoredSession(token, claims, lang);
        }
        catch (JSException)
        {
            return null;
        }
    }

    public async ValueTask ClearAsync()
    {
        try
        {
            await js.InvokeVoidAsync("devArchLocalStorage.removeItem", TokenKey);
            await js.InvokeVoidAsync("devArchLocalStorage.removeItem", ClaimsKey);
            await js.InvokeVoidAsync("devArchLocalStorage.removeItem", LangKey);
        }
        catch (JSException)
        {
            // Ignore
        }
    }
}

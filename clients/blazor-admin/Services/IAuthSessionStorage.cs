namespace Blazor.Admin.Services;

public interface IAuthSessionStorage
{
    ValueTask SaveAsync(string token, IReadOnlyList<string> claims, string lang);
    ValueTask<StoredSession?> TryLoadAsync();
    ValueTask ClearAsync();
}

public readonly record struct StoredSession(string Token, string[] Claims, string Lang);

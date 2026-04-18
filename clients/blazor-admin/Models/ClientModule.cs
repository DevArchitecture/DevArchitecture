namespace Blazor.Admin.Models;

public sealed record ClientModule(string Key, string Label, string Route, string? Endpoint = null);

public static class ClientModules
{
    public static readonly IReadOnlyList<ClientModule> All =
    [
        new("dashboard", "Dashboard", "/dashboard"),
        new("user", "Users", "/user", "/users"),
        new("group", "Groups", "/group", "/groups"),
        new("language", "Languages", "/language", "/languages"),
        new("translate", "Translates", "/translate", "/translates"),
        new("operationclaim", "Operation Claims", "/operationclaim", "/operation-claims"),
        new("log", "Logs", "/log", "/logs")
    ];
}

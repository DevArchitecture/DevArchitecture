namespace Core.Settings;
public static class DevArchitectureSettings
{
    public static Loggers Loggers { get; set; } = new();
    public static Priorities Priorities { get; set; } = new();
    public static Intervals Intervals { get; set; } = new();
    public static Durations Durations { get; set; } = new();
}
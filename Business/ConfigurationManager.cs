using Microsoft.Extensions.Hosting;

namespace Business;

public enum ApplicationMode
{
    Development,
    Profiling,
    Staging,
    Production,
}

public class ConfigurationManager
{

    public ConfigurationManager(IHostEnvironment env)
    {
        Mode = (ApplicationMode)Enum.Parse(typeof(ApplicationMode), env.EnvironmentName);
    }

    public ApplicationMode Mode { get; private set; }
}

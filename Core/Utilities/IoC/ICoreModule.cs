namespace Core.Utilities.IoC
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public interface ICoreModule
    {
        void Load(IServiceCollection services, IConfiguration configuration);
    }
}

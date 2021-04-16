namespace WebAPI.EntityBaseOverride
{
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    ///
    /// </summary>
    public class ServiceInjection : IDesignTimeServices
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceCollection"></param>
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ICSharpEntityTypeGenerator, OverrideBase>();
        }
    }
}


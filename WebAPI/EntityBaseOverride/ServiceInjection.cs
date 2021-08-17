using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.EntityBaseOverride
{
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
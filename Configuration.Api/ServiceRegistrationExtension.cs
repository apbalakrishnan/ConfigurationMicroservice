using Configuration.Repository.ClientInfoSetup;
using Configuration.Repository.VerticalInfoSetup;

namespace Configuration.Api
{
    public static class ServiceRegistrationExtension
    {
        public static IServiceCollection ServiceCollectionExtension(this IServiceCollection services)
        {
            services.AddTransient<IVerticalService, VerticalService>();
            services.AddTransient<IClientInfoSetupService, ClientInfoSetupService>();
            return services;
        }
    }
}

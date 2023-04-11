using Configuration.Repository;
using Configuration.RepositoryHandler.MsSql.EF.ClientInfoSetup;
using Configuration.RepositoryHandler.MsSql.EF.VerticalInfoSetup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Configuration.RepositoryHandler.MsSql.EF.CoreSql
{
    public static class RepositoryServiceRegistration
    {
        public static IServiceCollection AddSqlEfRepositoryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("SqlConnectionString"), sqlServerOptionsAction: sqloptions =>
                {
                    sqloptions.EnableRetryOnFailure
                    (
                         maxRetryCount: Convert.ToInt32(configuration.GetSection("Resilience:MaxRetryCount").Value ?? "2"),
                         maxRetryDelay: TimeSpan.FromSeconds(Convert.ToInt32(configuration.GetSection("Resilience::MaxRetryDelay").Value ?? "20")),
                         errorNumbersToAdd: null
                    );
                }));

            services.AddScoped(typeof(IAsyncRepositoryOperations<>), typeof(BaseRepositoryOperations<>));
            services.AddScoped<IVerticalInfoOperations, VerticalInfoOperations>();
            services.AddScoped<IClientInfoSetupOperations, ClientInfoSetupOperations>();
            
            return services;
        }
    }
}

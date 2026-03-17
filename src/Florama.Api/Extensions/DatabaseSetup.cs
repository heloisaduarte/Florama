using Florama.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Florama.Api.Extensions
{
    public static class DatabaseSetup
    {
        public static IServiceCollection AddDatabaseSetup(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<FloramaDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("Florama.Data");
                    sqlOptions.CommandTimeout(35);
                    sqlOptions.EnableRetryOnFailure(3);
                });

#if DEBUG
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
#endif
            });

            return services;
        }
    }
}
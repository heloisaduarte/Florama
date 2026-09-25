using Florama.Data;
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
                    sqlOptions.CommandTimeout(60);
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 6,
                        maxRetryDelay: TimeSpan.FromSeconds(15),
                        errorNumbersToAdd: new[] { 40925, 40613, 40501, 40197 });
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
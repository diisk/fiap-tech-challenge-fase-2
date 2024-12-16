using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DbContexts
{
    public static class DbContextOptionsConfigurator
    {
        public static DbContextOptions<T> Create<T>(string appSettingsProjectName) where T : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<T>();
            Configure(optionsBuilder, appSettingsProjectName);
            return optionsBuilder.Options;
        }

        public static void Configure(DbContextOptionsBuilder optionsBuilder, string appSettingsProjectName)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, appSettingsProjectName))
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{environment}.json", optional: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("ConnectionString");

                optionsBuilder.UseMySql("Server=mysql;Database=tc1_contatos_regionais;User=root;Password=123456;Port=3306;", new MySqlServerVersion(new Version(8, 0, 25)));
                //optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 25)));
                optionsBuilder.UseLazyLoadingProxies(true);
            }
            
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DbContexts
{
    public static class DbContextOptionsConfigurator
    {
        public static DbContextOptions<T> Create<T>() where T : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<T>();
            Configure(optionsBuilder);
            return optionsBuilder.Options;
        }

        public static void Configure(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")??"Development";
                var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__ConnectionString");
                if (enviroment.ToUpper()=="DEVELOPMENT")
                {
                    IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                    connectionString = configuration.GetConnectionString("ConnectionString");
                }
                
                optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 25)));
                //optionsBuilder.UseLazyLoadingProxies(true);
            }

        }
    }
}

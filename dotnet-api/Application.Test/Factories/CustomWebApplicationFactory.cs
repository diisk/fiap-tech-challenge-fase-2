
using Infrastructure.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MySql;

namespace Application.Test.Factories
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly MySqlContainer container;

        public CustomWebApplicationFactory(MySqlContainer container)
        {
            this.container = container;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var onlyReadDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<OnlyReadDbContext>));
                if (onlyReadDescriptor != null)
                {
                    services.Remove(onlyReadDescriptor);
                }

                services.AddDbContext<OnlyReadDbContext>(options =>
                    options.UseMySql(container.GetConnectionString(),
                                     ServerVersion.AutoDetect(container.GetConnectionString())));

                var onlyWriteDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<OnlyWriteDbContext>));
                if (onlyWriteDescriptor != null)
                {
                    services.Remove(onlyWriteDescriptor);
                }

                services.AddDbContext<OnlyWriteDbContext>(options =>
                    options.UseMySql(container.GetConnectionString(),
                                     ServerVersion.AutoDetect(container.GetConnectionString())));
            });
        }
    }
}

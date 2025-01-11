using System.Threading.Tasks;
using Testcontainers.MySql;

namespace Application.Test.Factories
{
    public static class MySqlContainerFactory
    {
        public static MySqlContainer CreateMySqlContainer()
        {
            var config = new MySqlConfiguration("tc1_contatos_regionais", "testuser", "testpassword");

            var containerBuilder = new MySqlBuilder()
                .WithDatabase(config.Database)
                .WithImage("mysql:8.0")
                .WithCleanUp(true);
                //.WithName("mysql_test_container")
                //.WithPortBinding(3307, 3306);

            var container = containerBuilder.Build();

            return container;
        }
    }
}

using Microsoft.Extensions.Configuration;
using Npgsql;
using StudentManagementSystem.Repositories;

namespace StudentManagementSystem
{
    class Program
    {
        static async Task Main()
        {
            // Load configuration from user secrets to retrieve the database connection string.
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            string? connectionString =
                configuration.GetConnectionString("StudentDatabase");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                Console.WriteLine("Database connection string is missing.");
                return;
            }

            await using var dataSource =  NpgsqlDataSource.Create(connectionString);

            try
            {
                await using var connection =
                    await dataSource.OpenConnectionAsync();

                Console.WriteLine("Connected to PostgreSQL successfully!");
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Database connection failed: {ex.Message}");
                return;
            }

            StudentRepository repository =
                new StudentRepository(dataSource);

            StudentService service =
                new StudentService(repository);

            Menu menu = new Menu(service);

            menu.DisplayMenu();
        }
    }
}
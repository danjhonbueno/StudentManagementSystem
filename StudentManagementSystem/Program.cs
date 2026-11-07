using Npgsql;
using StudentManagementSystem.Repositories;

namespace StudentManagementSystem
{
    class Program
    {
        static async Task Main()
        {
            string connectionString =
                "Host=localhost;Port=5432;Database=student_management_db;Username=postgres;Password=10272821";

            await using var dataSource = NpgsqlDataSource.Create(connectionString);

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
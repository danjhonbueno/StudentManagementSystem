using Npgsql;

namespace StudentManagementSystem.Repositories
{
    public class StudentRepository
    {
        private readonly NpgsqlDataSource _dataSource;

        public StudentRepository(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }
    }
}
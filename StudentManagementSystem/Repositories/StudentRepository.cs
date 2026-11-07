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

        // This method retrieves all student records from the database asynchronously.
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            // Open a connection to the PostgreSQL database using the provided data source.
            await using var connection =
                await _dataSource.OpenConnectionAsync();

            // Define the SQL query to select all records from the "students" table.
            string selectAllStudentsQuery = "SELECT * FROM students;";

            // Create a command to execute the SQL query against the database.
            await using var command = new NpgsqlCommand(selectAllStudentsQuery, connection);

            // Execute the command and obtain a reader to read the results asynchronously.
            await using var reader = await command.ExecuteReaderAsync();

            // Initialize a list to hold the retrieved student records.
            List<Student> students = new List<Student>();

            // Process the results.
            while (await reader.ReadAsync())
            {
                // Access the values from the current row.
                var student_id = reader.GetInt32(reader.GetOrdinal("student_id"));
                var first_name = reader.GetString(reader.GetOrdinal("first_name"));
                var last_name = reader.GetString(reader.GetOrdinal("last_name"));
                var age = reader.GetInt32(reader.GetOrdinal("age"));
                var grade_level = reader.GetString(reader.GetOrdinal("grade_level"));
                var section = reader.GetString(reader.GetOrdinal("section"));

                // Create a new Student object and populate it with the retrieved values.
                var student = new Student
                {
                    StudentId = student_id,
                    FirstName = first_name,
                    LastName = last_name,
                    Age = age,
                    GradeLevel = grade_level,
                    Section = section
                };
                students.Add(student);
            }
            return students;
        }
    }


}
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

            await using var connection =
                await _dataSource.OpenConnectionAsync();

            string selectAllStudentsQuery = "SELECT * FROM students;";

            await using var command = new NpgsqlCommand(selectAllStudentsQuery, connection);

            await using var reader = await command.ExecuteReaderAsync();

            // Initialize a list to hold the retrieved student records.
            List<Student> students = new List<Student>();

            // Process the results.
            while (await reader.ReadAsync())
            {
                // Access the values from the current row.
                var studentId = reader.GetInt32(reader.GetOrdinal("student_id"));
                var firstName = reader.GetString(reader.GetOrdinal("first_name"));
                var lastName = reader.GetString(reader.GetOrdinal("last_name"));
                var age = reader.GetInt32(reader.GetOrdinal("age"));
                var gradeLevel = reader.GetString(reader.GetOrdinal("grade_level"));
                var section = reader.GetString(reader.GetOrdinal("section"));

                // Create a new Student object and populate it with the retrieved values.
                var student = new Student
                {
                    StudentId = studentId,
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age,
                    GradeLevel = gradeLevel,
                    Section = section
                };
                students.Add(student);
            }
            return students;
        }

        public async Task<Student?> GetStudentByIdAsync(int studentId)
        {
            await using var connection =
                await _dataSource.OpenConnectionAsync();

            string selectStudentByIdQuery = "SELECT student_id, first_name, last_name, age, grade_level, section\r\nFROM students\r\nWHERE student_id = @studentId;";

            await using var command = new NpgsqlCommand(selectStudentByIdQuery, connection);
            command.Parameters.AddWithValue("studentId", studentId);

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var student = new Student
                {
                    StudentId = reader.GetInt32(reader.GetOrdinal("student_id")),
                    FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                    LastName = reader.GetString(reader.GetOrdinal("last_name")),
                    Age = reader.GetInt32(reader.GetOrdinal("age")),
                    GradeLevel = reader.GetString(reader.GetOrdinal("grade_level")),
                    Section = reader.GetString(reader.GetOrdinal("section"))
                };
                return student;
            }
            else
            {
                return null;
            }

        }

        public async Task<bool> UpdateStudentAsync(Student student)
        {
            await using var connection =
                await _dataSource.OpenConnectionAsync();

            string updateStudentQuery = "UPDATE students SET first_name = @firstName, last_name = @lastName, age = @age, grade_level = @gradeLevel, section = @section WHERE student_id = @studentId;";

            await using var command = new NpgsqlCommand(updateStudentQuery, connection);

            command.Parameters.AddWithValue("firstName", student.FirstName);
            command.Parameters.AddWithValue("lastName", student.LastName);
            command.Parameters.AddWithValue("age", student.Age);
            command.Parameters.AddWithValue("gradeLevel", student.GradeLevel);
            command.Parameters.AddWithValue("section", student.Section);
            command.Parameters.AddWithValue("studentId", student.StudentId);

            int rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;

        }

    }


}
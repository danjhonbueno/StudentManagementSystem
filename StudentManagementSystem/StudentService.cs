namespace StudentManagementSystem;
using StudentManagementSystem.Repositories;

public class StudentService
{
    // The AddStudent method will prompt the user to enter details for a new student, create a Student object, and then call the repository to add the student to the database. It will also display a confirmation message with the new student's ID.
    public async Task<Student> AddStudent()
    {
        Student student = new Student();

        Console.WriteLine("\nAdding a new student...\n");

        student.FirstName = ReadNameInput("First name: ");
        student.LastName = ReadNameInput("Last name: ");
        student.Age = ReadIntInput("Age: ", 1,100);
        student.GradeLevel = ReadRequiredText("Grade Level: ");
        student.Section = ReadRequiredText("Section: ");

            int newStudentId = await _studentRepository.AddStudentAsync(student);
            student.StudentId = newStudentId;
            return student;
    }

    // The ListAll method will retrieve all student records from the database and display them in a formatted manner. If no records are found, it will inform the user accordingly.
    public async Task ListAll()
    {
        var students = await GetAllStudentsAsync();
        if (students.Count == 0)
        {
            Console.WriteLine("No student records found.");
            return;
        }

        Console.WriteLine("\nStudent Records:");
        foreach (var student in students)
        {
            DisplayStudentDetails(student);
        }
    }

    // The FindById method will prompt the user to enter a student ID, retrieve the corresponding student record from the database, and display the details. If the student is not found, it will inform the user accordingly.
    public async Task FindById()
    {
        int searchId = ReadIntInput("\nEnter the Student ID to search: ");
        var student = await _studentRepository.GetStudentByIdAsync(searchId);

        if (student != null)
        {
            DisplayStudentDetails(student);
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    // The UpdateStudent method will prompt the user to enter a student ID, retrieve the corresponding student record, and allow the user to update specific fields. After updating, it will call the repository to save the changes to the database and display a confirmation message.
    public async Task<bool> UpdateStudent()
    {
        int searchId = ReadIntInput("Enter the Student ID to search: ");
        var student = await _studentRepository.GetStudentByIdAsync(searchId);

        if (student != null)
        {
            Console.WriteLine("\nUpdating student information...");
            Console.WriteLine("\nWhich specific field would you like to update?\n");
            Console.WriteLine("1. First Name");
            Console.WriteLine("2. Last Name");
            Console.WriteLine("3. Age");
            Console.WriteLine("4. Grade Level");
            Console.WriteLine("5. Section");

            int fieldChoice = ReadIntInput("\nEnter your choice: ", 1, 5);
            Console.WriteLine($"\nUpdating record for Student ID: {student.StudentId}\n");

            switch (fieldChoice)
            {
                case 1:
                    student.FirstName = ReadNameInput("Enter new first name: ");
                    break;
                case 2:
                    student.LastName = ReadNameInput("Enter new last name: ");
                    break;
                case 3:
                    student.Age = ReadIntInput("Enter new age: ", 1, 100);
                    break;
                case 4:
                    student.GradeLevel = ReadRequiredText("Enter new grade level: ");
                    break;
                case 5:
                    student.Section = ReadRequiredText("Enter new section: ");
                    break;
            }

            bool isUpdated = await _studentRepository.UpdateStudentAsync(student);

            if (!isUpdated)
            {
                Console.WriteLine("Failed to update student.");
                return false;
            }
            Console.WriteLine("Student updated successfully.");
            return true;
        }
        else
        {
            Console.WriteLine("Student not found.");
            return false;
        }
    }

    // The DeleteStudent method will allow the user to delete a student's record by their ID. If the student is found, the user will be asked to confirm the deletion. If confirmed, the student will be removed from the list; otherwise, the deletion will be canceled. If the student is not found, a message will be displayed.
    public async Task<bool> DeleteStudent()
    {
        int searchId = ReadIntInput("Enter the Student ID to delete: ");
        var student = await _studentRepository.GetStudentByIdAsync(searchId);

        if (student != null)
        {
            Console.Write("Are you sure you want to delete this student? (y/n): ");
            string confirm = Console.ReadLine();

            if (string.Equals(confirm?.Trim(), "y", StringComparison.OrdinalIgnoreCase))
            {
                bool isDeleted = await _studentRepository.DeleteStudentAsync(searchId);

                if (!isDeleted)
                {
                    Console.WriteLine("Failed to delete student.");
                    return false;
                }
                Console.WriteLine("Student deleted successfully.");
                return true;

            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
                return false;
            }
        }
        else
        {
            Console.WriteLine("Student not found.");
            return false;
        }
    }

    // The GetAllStudentsAsync method will retrieve all student records from the database by calling the corresponding method in the StudentRepository. It returns a list of Student objects.
    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _studentRepository.GetAllStudentsAsync();
    }

    // For the ReadIntInput method, we will ensure that the user cannot enter a non-integer value for the student ID and age.
    private int ReadIntInput(string prompt, int minValue = int.MinValue, int maxValue = int.MaxValue)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out value))
            {
                if (value >= minValue && value <= maxValue)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine($"Input out of range. Please enter a value between {minValue} and {maxValue}.\n");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.\n");
            }
        }
    }

    // For the ReadStringInput method, we will ensure that the user cannot enter an empty string for the section.
    // If the user tries to do so, we will prompt them again until they provide a valid input.
    private string ReadRequiredText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            // Check that the user didn't just press Enter on an empty line
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input cannot be empty.\n");
                continue;
            }
            input = input.Trim();
            return input;
        }
    }

    private string ReadNameInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            // Check that the user didn't just press Enter on an empty line
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input cannot be empty.\n");
                continue;
            }

            bool isValid = true;
            foreach (char c in input)
            {
                if (!char.IsLetter(c) && c != ' ' && c != '-' && c != '\'')
                {
                    Console.WriteLine("Input can only contain letters, spaces, hyphens, and apostrophes.\n");
                    isValid = false;
                    break;
                }
            }
            if (!isValid)
            {
                continue;
            }
            input = input.Trim();
            return input;
        }
    }

    private void DisplayStudentDetails(Student student)
    {
        Console.WriteLine($"\nID: {student.StudentId}, Name: {student.FirstName} {student.LastName}, Age: {student.Age}, Grade Level: {student.GradeLevel}, Section: {student.Section}");
    }

    private readonly StudentRepository _studentRepository;

    public StudentService(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;

    }
}
using System;
using System.Collections.Generic;

public class StudentService
{
    private List<Student> students = new List<Student>();
    private List<Student> foundStudents = new List<Student>();

    public void AddStudent()
	{
        Student student = new Student();

        Console.WriteLine("");
        Console.WriteLine("Adding a new student...");
        Console.WriteLine("");

        student.StudentId = ReadIntInput("Student Id: ");

        Console.Write("First name: ");
        student.FirstName = Console.ReadLine();

        Console.Write("Last name: ");
        student.LastName = Console.ReadLine();

        student.Age = ReadIntInput("Age: ");
        student.GradeLevel = ReadStringInput("Grade Level: ");

        student.Section = ReadStringInput("Section: ");

        students.Add(student);
        Console.WriteLine("\nStudent added successfully!");
    }
    public void ViewStudent()
    {
        if (students == null || students.Count == 0)
        {
            Console.WriteLine("No student records found.");
            return;
        }
        else
        {
            Console.WriteLine("Student Records:");
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.StudentId}, Name: {student.FirstName} {student.LastName}, Age: {student.Age}, Grade Level: {student.GradeLevel}, Section: {student.Section}");
            }
        }   
    }
    public void SearchStudent()
    {
        if (students == null || students.Count == 0)
        {
            Console.WriteLine("No student records found.");
            return;
        }

        int searchId = ReadIntInput("Enter the Student ID to search: ");
        var student = students.Find(s => s.StudentId == searchId);
        if (student != null)
        {
            Console.WriteLine($"ID: {student.StudentId}, Name: {student.FirstName} {student.LastName}, Age: {student.Age}, Grade Level: {student.GradeLevel}, Section: {student.Section}");
        }
        else
        {
            Console.WriteLine("Student not found.");
            }
    }
    public void UpdateStudent()
    {
        if (students == null || students.Count == 0)
        {
            Console.WriteLine("No student records found.");
            return;
        }
        int searchId = ReadIntInput("Enter the Student ID to search: ");
        var student = students.Find(s => s.StudentId == searchId);
        if (student != null)
        {
            Console.WriteLine("Updating student information...");
            Console.WriteLine("Which specific field would you like to update?");
            Console.WriteLine("1. First Name");
            Console.WriteLine("2. Last Name");
            Console.WriteLine("3. Age");
            Console.WriteLine("4. Grade Level");
            Console.WriteLine("5. Section");
            Console.Write("Enter your choice: ");

            if (int.TryParse(Console.ReadLine(), out int fieldChoice))
            {
                Console.WriteLine($"Updating record for Student ID: {student.StudentId}\n");
                switch (fieldChoice)
                {
                    case 1:
                        Console.Write("Enter new first name: ");
                        student.FirstName = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Enter new last name: ");
                        student.LastName = Console.ReadLine();
                        break;
                    case 3:
                        student.Age = ReadIntInput("Enter new age: ");
                        break;
                    case 4:
                        student.GradeLevel = ReadStringInput("Enter new grade level: ");
                        break;
                    case 5:
                        student.Section = ReadStringInput("Enter new section: ");
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }
    public void DeleteStudent()
    {

    }
    // For the ReadIntInput method, we will ensure that the user cannot enter a non-integer value for the student ID and age.
    private int ReadIntInput(string prompt)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out value))
            {
                return value;
            }

            Console.WriteLine("Invalid input. Please enter a valid integer.\n");
        }
    }
    // For the ReadStringInput method, we will ensure that the user cannot enter an empty string for the section.
    // If the user tries to do so, we will prompt them again until they provide a valid input.
    private string ReadStringInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            // Check that the user didn't just press Enter on an empty line
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim(); // Removes extra leading or trailing spaces
            }

            Console.WriteLine("Invalid input. Section cannot be empty.\n");
        }
    }
}

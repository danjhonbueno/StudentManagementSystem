using System;
using System.Collections.Generic;

public class StudentService
{
    private List<Student> students = new List<Student>();

    public void AddStudent()
    {
        Student student = new Student();

        Console.WriteLine("\nAdding a new student...\n");

        // Check if the student ID already exists in the list of students. If it does, prompt the user to enter a different ID.
        while (true)
        {
            int id = ReadIntInput("Student Id: ", 1, 9999);
            if (students.Exists(s => s.StudentId == id))
            {
                Console.WriteLine("A student with this ID already exists. Please try a different ID.\n");
            }
            else
            {
                student.StudentId = id;
                break;
            }
        }

        student.FirstName = ReadNameInput("First name: ");
        student.LastName = ReadNameInput("Last name: ");
        student.Age = ReadIntInput("Age: ", 1,100);
        student.GradeLevel = ReadStringInput("Grade Level: ");
        student.Section = ReadStringInput("Section: ");

        students.Add(student);
        Console.WriteLine("\nStudent added successfully!");
    }

    // The ViewStudent method will display all student records in a formatted manner. If there are no student records, it will inform the user accordingly.
    public void ViewStudent()
    {
        if (students == null || students.Count == 0)
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

    // The SearchStudent method will allow the user to search for a student by their ID. If the student is found, their details will be displayed; otherwise, a message indicating that the student was not found will be shown.
    public void SearchStudent()
    {
        if (students == null || students.Count == 0)
        {
            Console.WriteLine("No student records found.");
            return;
        }

        int searchId = ReadIntInput("\nEnter the Student ID to search: ");
        var student = students.Find(s => s.StudentId == searchId);

        if (student != null)
        {
            DisplayStudentDetails(student);
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    // The UpdateStudent method will allow the user to update specific fields of a student's record. The user will be prompted to select which field they want to update, and then they can enter the new value for that field. If the student is not found, a message will be displayed.
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
                    Console.WriteLine("\nFirst name updated successfully.");
                    break;
                case 2:
                    student.LastName = ReadNameInput("Enter new last name: ");
                    Console.WriteLine("\nLast name updated successfully.");
                    break;
                case 3:
                    student.Age = ReadIntInput("Enter new age: ", 1, 100);
                    Console.WriteLine("\nAge updated successfully.");
                    break;
                case 4:
                    student.GradeLevel = ReadStringInput("Enter new grade level: ");
                    Console.WriteLine("\nGrade level updated successfully.");
                    break;
                case 5:
                    student.Section = ReadStringInput("Enter new section: ");
                    Console.WriteLine("\nSection updated successfully.");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    // The DeleteStudent method will allow the user to delete a student's record by their ID. If the student is found, the user will be asked to confirm the deletion. If confirmed, the student will be removed from the list; otherwise, the deletion will be canceled. If the student is not found, a message will be displayed.
    public void DeleteStudent()
    {
        if (students == null || students.Count == 0)
        {
            Console.WriteLine("No student records found.");
            return;
        }

        int searchId = ReadIntInput("Enter the Student ID to delete: ");
        var student = students.Find(s => s.StudentId == searchId);

        if (student != null)
        {
            Console.Write("Are you sure you want to delete this student? (y/n): ");
            string confirm = Console.ReadLine();

            if (confirm?.ToLower() == "y")
            {
                students.Remove(student);
                Console.WriteLine("Student deleted successfully.");
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
            }
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
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
    private string ReadStringInput(string prompt)
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
                if (!char.IsLetter(c) && c != ' ')
                {
                    Console.WriteLine("Input can only contain letters and spaces.\n");
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
}
using System;
using System.Collections.Generic;

public class StudentService
{
    private List<Student> students = new List<Student>();

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

        students.Add(student);
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

    }
    public void UpdateStudent()
    {

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

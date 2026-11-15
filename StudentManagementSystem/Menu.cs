using System;
using System.IO;

namespace StudentManagementSystem
{
    public class Menu
    {
        private readonly StudentService studentService;

        public Menu(StudentService studentService)
        {
            this.studentService = studentService;
        }

        public async Task DisplayMenu()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== Welcome to the Student Management System ===");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View Student");
                Console.WriteLine("3. Search Student");
                Console.WriteLine("4. Update Student");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("6. Exit");
                Console.Write("\nEnter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid choice! Please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:

                        try
                        {   
                            Student result = await studentService.AddStudent();
                            Console.WriteLine($"Student {result.StudentId} added successfully!");
                        }
                        catch (Exception ex)
                        {
                            File.AppendAllText("error_log.txt", $"[{DateTime.Now}] Error adding student: {ex.ToString()}{Environment.NewLine}");
                            Console.WriteLine("An error occurred while adding the student. Please check the error log for more details.");
                            Console.WriteLine($"Log file location: {Path.GetFullPath("error_log.txt")}");
                        }
                        break;
                    case 2:
                        await studentService.ListAll();
                        break;
                    case 3:
                        await studentService.FindById();
                        break;
                    case 4:
                        await studentService.UpdateStudent();
                        break;
                    case 5:
                        await studentService.DeleteStudent();
                        break;
                    case 6:
                        Console.WriteLine("Exiting program... Goodbye!");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select 1 through 6.");
                        break;
                }
            }
        }
    }
}
using System;
using System.Globalization;
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
                            LogError(ex);
                            Console.WriteLine("An error occurred while adding the student. Please check the error log for more details.");
                        }
                        break;
                    case 2:

                        try
                        {
                            await studentService.ListAll();
                        }
                        catch (Exception ex)
                        {
                            LogError(ex);
                            Console.WriteLine("An error occurred while viewing students. Please check the error log for more details.");
                        }
                        break;
                    case 3:

                        try
                        {
                            await studentService.FindById();
                        }
                        catch (Exception ex)
                        {
                            LogError(ex);
                            Console.WriteLine("An error occurred while searching for the student. Please check the error log for more details.");
                        }
                        break;
                    case 4:

                        try
                        {
                            await studentService.UpdateStudent();
                        }
                        catch (Exception ex)
                        {
                            LogError(ex);
                            Console.WriteLine("An error occurred while updating the student. Please check the error log for more details.");
                        }
                        break;
                    case 5:
                        try
                        {
                            await studentService.DeleteStudent();
                        }
                        catch (Exception ex)
                        {
                            LogError(ex);
                            Console.WriteLine("An error occurred while deleting the student. Please check the error log for more details.");
                        }
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

        private void LogError(Exception ex)
        {
            string logFilePath = "error_log.txt";
            string logMessage = $"[{DateTime.Now}] {ex.ToString()}{Environment.NewLine}";
            File.AppendAllText(logFilePath, logMessage);
        }
    }
    
}
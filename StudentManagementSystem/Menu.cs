namespace StudentManagementSystem
{
    public class Program
    {
        static void Main()
        {
            Menu menu = new Menu();
            menu.DisplayMenu();
        }
    }
}

namespace StudentManagementSystem
{
    public class Menu
    {
        private StudentService studentService = new StudentService();

        public void DisplayMenu()
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
                        studentService.AddStudent();
                        break;
                    case 2:
                        studentService.ListAll();
                        break;
                    case 3:
                        studentService.FindById();
                        break;
                    case 4:
                        studentService.UpdateStudent();
                        break;
                    case 5:
                        studentService.DeleteStudent();
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
using TaskManagerApp.Models;
using TaskManagerApp.Services;

var taskService = new TaskService();

while (true)
{
    Console.Clear();
    Console.WriteLine("=== Task Manager ===");
    Console.WriteLine("1. View Tasks");
    Console.WriteLine("2. Add Task");
    Console.WriteLine("3. Mark Task Complete");
    Console.WriteLine("4. Exit");
    Console.Write("\nSelect an option: ");
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Clear();
            Console.WriteLine("Your Tasks:");
            foreach (var t in taskService.GetTasks())
                Console.WriteLine(t);
            Console.WriteLine("\nPress Enter to return...");
            Console.ReadLine();
            break;

        case "2":
            Console.Clear();
            Console.Write("Title: ");
            var title = Console.ReadLine() ?? "";
            Console.Write("Description: ");
            var desc = Console.ReadLine() ?? "";
            Console.Write("Due Date (yyyy-mm-dd): ");
            DateTime.TryParse(Console.ReadLine(), out var dueDate);
            taskService.AddTask(new TaskItem
            {
                Title = title,
                Description = desc,
                DueDate = dueDate,
                IsCompleted = false
            });
            Console.WriteLine("Task added! Press Enter...");
            Console.ReadLine();
            break;

        case "3":
            Console.Clear();
            Console.Write("Enter Task ID to mark complete: ");
            if (int.TryParse(Console.ReadLine(), out var id))
                taskService.MarkComplete(id);
            Console.WriteLine("Updated! Press Enter...");
            Console.ReadLine();
            break;

        case "4":
            return;

        default:
            Console.WriteLine("Invalid choice.");
            Console.ReadLine();
            break;
    }
}
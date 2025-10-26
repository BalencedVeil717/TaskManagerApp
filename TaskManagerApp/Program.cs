using TaskManagerApp.Models;
using TaskManagerApp.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var taskService = new TaskService();

while (true)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("=== Task Manager ===");
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\n1. View Tasks");
    Console.WriteLine("2. Add Task");
    Console.WriteLine("3. Mark Task Complete");
    Console.WriteLine("4. Exit");
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.Write("\nSelect an option: ");
    Console.ResetColor();
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("───── Your Tasks ─────");
            Console.ResetColor();

            var tasks = taskService.GetTasks().ToList();
            if (tasks.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("No tasks yet! Add one from the menu.");
                Console.ResetColor();
            }
            else
            {
                foreach (var t in tasks)
                {
                    if (t.IsCompleted)
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    else if (t.DueDate < DateTime.Now)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.Gray;

                    Console.Write($"[{t.Id}] ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(t.Title);
                    Console.ResetColor();

                    Console.Write(" — ");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine(t.IsCompleted ? "[✔]" : $"Due: {t.DueDate:yyyy-MM-dd}");
                    Console.ResetColor();
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress Enter to return...");
            Console.ResetColor();
            Console.ReadLine();
            break;

        case "2":
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("───── Add New Task ─────\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Title: ");
            Console.ResetColor();
            var title = Console.ReadLine() ?? "";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Description: ");
            Console.ResetColor();
            var desc = Console.ReadLine() ?? "";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Due Date (yyyy-mm-dd): ");
            Console.ResetColor();
            DateTime.TryParse(Console.ReadLine(), out var dueDate);

            taskService.AddTask(new TaskItem
            {
                Title = title,
                Description = desc,
                DueDate = dueDate,
                IsCompleted = false
            });

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Task added successfully!");
            Console.ResetColor();
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
            break;

        case "3":
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("───── Mark Task Complete ─────\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter Task ID: ");
            Console.ResetColor();
            if (int.TryParse(Console.ReadLine(), out var id))
            {
                taskService.MarkComplete(id);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n✅ Task marked as complete!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ Invalid Task ID!");
            }

            Console.ResetColor();
            Console.WriteLine("\nPress Enter to return...");
            Console.ReadLine();
            break;

        case "4":
            return;

        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ Invalid choice. Please try again.");
            Console.ResetColor();
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
            break;
    }
}
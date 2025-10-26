using System.Text.Json;
using TaskManagerApp.Models;

namespace TaskManagerApp.Services;

public class TaskService
{
    private readonly string _filePath = "tasks.json";
    private List<TaskItem> _tasks = new();

    public TaskService()
    {
        LoadTasks();
    }

    public void AddTask(TaskItem task)
    {
        task.Id = _tasks.Count == 0 ? 1 : _tasks.Max(t => t.Id) + 1;
        _tasks.Add(task);
        SaveTasks();
    }

    public List<TaskItem> GetTasks() => _tasks;

    public void MarkComplete(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return;
        task.IsCompleted = true;
        SaveTasks();
    }

    private void SaveTasks()
    {
        var json = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    private void LoadTasks()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            _tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }
    }
}
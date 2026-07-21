using space_lasers.Models;

namespace space_lasers.Services;

public sealed class InMemoryTodoRepository
{
    private readonly List<TodoItem> _items = [];
    private readonly Lock _sync = new();

    public IReadOnlyList<TodoItem> GetAll()
    {
        lock (_sync)
        {
            return _items
                .OrderBy(item => item.IsCompleted)
                .ThenBy(item => item.DueDate ?? DateOnly.MaxValue)
                .ThenByDescending(item => item.Priority)
                .ToArray();
        }
    }

    public void Add(string title, string category, TodoPriority priority, DateOnly? dueDate)
    {
        lock (_sync)
        {
            _items.Add(new TodoItem
            {
                Title = title.Trim(),
                Category = category,
                Priority = priority,
                DueDate = dueDate
            });
        }
    }

    public bool ToggleCompletion(Guid id)
    {
        lock (_sync)
        {
            var item = _items.FirstOrDefault(todo => todo.Id == id);
            if (item is null)
            {
                return false;
            }

            item.IsCompleted = !item.IsCompleted;
            return true;
        }
    }
}

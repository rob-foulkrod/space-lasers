namespace space_lasers.Models;

public enum TodoPriority
{
    Low,
    Medium,
    High
}

public sealed class TodoItem
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Title { get; init; }
    public required string Category { get; init; }
    public TodoPriority Priority { get; init; } = TodoPriority.Medium;
    public DateOnly? DueDate { get; init; }
    public bool IsCompleted { get; set; }
}

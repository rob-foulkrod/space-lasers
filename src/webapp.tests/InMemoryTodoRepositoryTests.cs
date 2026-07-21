using space_lasers.Models;
using space_lasers.Services;

namespace webapp.tests;

public sealed class InMemoryTodoRepositoryTests
{
    [Fact]
    public void Add_StoresTrimmedTodo()
    {
        var repository = new InMemoryTodoRepository();
        var dueDate = new DateOnly(2026, 8, 1);

        repository.Add("  Target asteroid  ", "Work", TodoPriority.High, dueDate);

        var item = Assert.Single(repository.GetAll());
        Assert.Equal("Target asteroid", item.Title);
        Assert.Equal("Work", item.Category);
        Assert.Equal(TodoPriority.High, item.Priority);
        Assert.Equal(dueDate, item.DueDate);
        Assert.False(item.IsCompleted);
    }

    [Fact]
    public void GetAll_OrdersByCompletionDueDateAndPriority()
    {
        var repository = new InMemoryTodoRepository();
        var earliestDueDate = new DateOnly(2026, 7, 22);

        repository.Add("Low priority", "Work", TodoPriority.Low, earliestDueDate);
        repository.Add("High priority", "Work", TodoPriority.High, earliestDueDate);
        repository.Add("No due date", "Home", TodoPriority.High, null);

        var completedItem = repository.GetAll()[1];
        Assert.True(repository.ToggleCompletion(completedItem.Id));

        Assert.Collection(
            repository.GetAll(),
            item => Assert.Equal("High priority", item.Title),
            item => Assert.Equal("No due date", item.Title),
            item => Assert.Equal("Low priority", item.Title));
    }

    [Fact]
    public void ToggleCompletion_ReturnsFalseForUnknownItem()
    {
        var repository = new InMemoryTodoRepository();

        Assert.False(repository.ToggleCompletion(Guid.NewGuid()));
    }

    [Fact]
    public void ToggleCompletion_TogglesExistingItem()
    {
        var repository = new InMemoryTodoRepository();
        repository.Add("Test laser", "Work", TodoPriority.Medium, null);
        var item = Assert.Single(repository.GetAll());

        Assert.True(repository.ToggleCompletion(item.Id));
        Assert.True(item.IsCompleted);
        Assert.True(repository.ToggleCompletion(item.Id));
        Assert.False(item.IsCompleted);
    }
}

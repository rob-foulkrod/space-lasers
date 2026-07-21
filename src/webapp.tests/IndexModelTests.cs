using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using space_lasers.Models;
using space_lasers.Pages;
using space_lasers.Services;

namespace webapp.tests;

public sealed class IndexModelTests
{
    [Fact]
    public void OnGet_LoadsTodos()
    {
        var repository = new InMemoryTodoRepository();
        repository.Add("Aim laser", "Work", TodoPriority.High, null);
        var model = new IndexModel(repository);

        model.OnGet();

        Assert.Single(model.TodoItems);
        Assert.Equal(["Work", "Personal", "Home", "Errands"], model.Categories);
        Assert.Equal(Enum.GetValues<TodoPriority>(), model.Priorities);
    }

    [Fact]
    public void OnPostAdd_WithInvalidInput_ReturnsPageAndLoadsTodos()
    {
        var repository = new InMemoryTodoRepository();
        repository.Add("Existing todo", "Home", TodoPriority.Low, null);
        var model = new IndexModel(repository);
        model.ModelState.AddModelError(nameof(IndexModel.Input.Title), "Required");

        var result = model.OnPostAdd();

        Assert.IsType<PageResult>(result);
        Assert.Single(model.TodoItems);
    }

    [Fact]
    public void OnPostAdd_WithValidInput_AddsTodoAndRedirects()
    {
        var repository = new InMemoryTodoRepository();
        var model = new IndexModel(repository)
        {
            Input = new IndexModel.NewTodoInput
            {
                Title = "Fire laser",
                Category = "Personal",
                Priority = TodoPriority.High
            }
        };

        var result = model.OnPostAdd();

        Assert.IsType<RedirectToPageResult>(result);
        var item = Assert.Single(repository.GetAll());
        Assert.Equal("Fire laser", item.Title);
        Assert.Equal("Personal", item.Category);
    }

    [Fact]
    public void OnPostAdd_WithUnknownCategory_UsesDefaultCategory()
    {
        var repository = new InMemoryTodoRepository();
        var model = new IndexModel(repository)
        {
            Input = new IndexModel.NewTodoInput
            {
                Title = "Charge laser",
                Category = "Unknown"
            }
        };

        model.OnPostAdd();

        Assert.Equal("Work", Assert.Single(repository.GetAll()).Category);
    }

    [Fact]
    public void OnPostToggle_TogglesTodoAndRedirects()
    {
        var repository = new InMemoryTodoRepository();
        repository.Add("Toggle laser", "Work", TodoPriority.Medium, null);
        var item = Assert.Single(repository.GetAll());
        var model = new IndexModel(repository);

        var result = model.OnPostToggle(item.Id);

        Assert.IsType<RedirectToPageResult>(result);
        Assert.True(item.IsCompleted);
    }
}

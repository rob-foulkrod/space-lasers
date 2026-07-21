using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using space_lasers.Models;
using space_lasers.Services;
using System.ComponentModel.DataAnnotations;

namespace space_lasers.Pages;

public class IndexModel : PageModel
{
    private static readonly string[] DefaultCategories = ["Work", "Personal", "Home", "Errands"];
    private readonly InMemoryTodoRepository _todoRepository;

    public IndexModel(InMemoryTodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public IReadOnlyList<TodoItem> TodoItems { get; private set; } = [];
    public IReadOnlyList<string> Categories => DefaultCategories;
    public IReadOnlyList<TodoPriority> Priorities { get; } = Enum.GetValues<TodoPriority>();

    [BindProperty]
    public NewTodoInput Input { get; set; } = new();

    public void OnGet()
    {
        LoadItems();
    }

    public IActionResult OnPostAdd()
    {
        if (!ModelState.IsValid)
        {
            LoadItems();
            return Page();
        }

        var category = DefaultCategories.Contains(Input.Category) ? Input.Category : DefaultCategories[0];
        _todoRepository.Add(Input.Title, category, Input.Priority, Input.DueDate);
        return RedirectToPage();
    }

    public IActionResult OnPostToggle(Guid id)
    {
        _todoRepository.ToggleCompletion(id);
        return RedirectToPage();
    }

    private void LoadItems()
    {
        TodoItems = _todoRepository.GetAll();
    }

    public sealed class NewTodoInput
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = DefaultCategories[0];

        public DateOnly? DueDate { get; set; }

        public TodoPriority Priority { get; set; } = TodoPriority.Medium;
    }
}

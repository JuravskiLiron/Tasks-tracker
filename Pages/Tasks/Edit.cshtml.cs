using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tasks_tracker.Data;
using Tasks_tracker.Models;

namespace Tasks_tracker.Pages.Tasks;

public class Edit(AppDbContext db) : PageModel
{
    [BindProperty] public TaskItem Item { get; set; } = default!;
    public async Task<IActionResult> OnGetAsync(int id)
    {
        var item = await db.Tasks.FindAsync(id);
        if(item is null) return NotFound();
        Item = item;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if(!ModelState.IsValid) return Page();
        
        var dbItem = await db.Tasks.FindAsync(Item.Id);
        if(dbItem is null) return NotFound();
        
        dbItem.Title = Item.Title;
        dbItem.Description = Item.Description;
        dbItem.DueAt = Item.DueAt;
        dbItem.EstimatedMinutes = Item.EstimatedMinutes; 
        dbItem.SpentMinutes = Item.SpentMinutes;
        dbItem.Priority = Item.Priority;
        dbItem.IsCompleted = Item.IsCompleted;
        dbItem.CompletedAt = Item.IsCompleted ? (dbItem.CompletedAt ?? DateTime.UtcNow) : null;
        await db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
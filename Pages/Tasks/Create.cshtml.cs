using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tasks_tracker.Models;
using Tasks_tracker.Data;

namespace Tasks_tracker.Pages.Tasks;

public class Create(AppDbContext db) : PageModel
{
    [BindProperty] public TaskItem Item { get; set; } = new();
    
    public void OnGet()
    {
         
        
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if(!ModelState.IsValid) return Page();
        Item.CreatedAt = DateTime.UtcNow;
        if(Item.IsCompleted && Item.CreatedAt == null) Item.CreatedAt = DateTime.UtcNow;
        db.Tasks.Add(Item);
        await db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
    
}
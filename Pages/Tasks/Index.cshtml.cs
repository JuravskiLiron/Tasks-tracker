using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tasks_tracker.Data;
using Tasks_tracker.Models;

namespace Tasks_tracker.Pages.Tasks;

public class Index : PageModel
{
    private readonly AppDbContext _db;

    public Index(AppDbContext db)
    {
        _db = db;
    }
    
    public IList<TaskItem> Items { get; set; } = new List<TaskItem>();
    
    [BindProperty(SupportsGet = true)]
    public bool ShowDone { get; set; } = false;

    public async Task OnGetAsync()
    {
        var query = _db.Tasks.AsQueryable();

        if (!ShowDone)
        {
            query = query.Where(t => !t.IsCompleted);
        }

        Items = await query
            .OrderBy(t => t.IsCompleted)
            .ThenByDescending(t => t.Priority)
            .ThenBy(t => t.DueAt ?? DateTime.MaxValue)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostToggleAsync(int id)
    {
        var item = await _db.Tasks.FindAsync(id);
        if(item == null)
            return NotFound();
        
        item.IsCompleted = !item.IsCompleted;
        item.CompletedAt = item.IsCompleted ? DateTime.UtcNow : null;
        
        await _db.SaveChangesAsync();
        return RedirectToPage(new { ShowDone });
    }

    public async Task<IActionResult> OnPostSpendAsync(int id, int minutes)
    {
        var item = await _db.Tasks.FindAsync(id);
        if(item == null)
            return NotFound();

        item.SpentMinutes = Math.Clamp((item.SpentMinutes ?? 0) + minutes, 0, 24 * 60);
        
        await _db.SaveChangesAsync();
        return RedirectToPage(new { ShowDone });
    }
    
}
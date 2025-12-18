using Microsoft.EntityFrameworkCore;
using Tasks_tracker.Models;

namespace Tasks_tracker.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
       public DbSet<TaskItem> Tasks => Set<TaskItem>();
    }
}

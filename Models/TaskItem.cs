using System.ComponentModel.DataAnnotations;

namespace Tasks_tracker.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(2000)]
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueAt { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
        //time mnagement
        [Range(0, 24 * 60)]
        public int? EstimatedMinutes { get; set; }
        [Range(0, 24 * 60)]
        public int? SpentMinutes { get; set; }
        [Range(0, 2)]
        public int Priority { get; set; } = 1;

    }
}

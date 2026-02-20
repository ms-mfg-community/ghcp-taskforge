using System.ComponentModel.DataAnnotations;

namespace TaskForge.Data.Models;

public class TaskItem
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    public Priority Priority { get; set; } = Priority.Medium;

    public TaskItemStatus Status { get; set; } = TaskItemStatus.Todo;

    public DateTime? DueDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;

    public string? AssignedToId { get; set; }
    public ApplicationUser? AssignedTo { get; set; }

    public string CreatedById { get; set; } = string.Empty;
    public ApplicationUser CreatedBy { get; set; } = null!;

    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<TaskLabel> TaskLabels { get; set; } = [];
}

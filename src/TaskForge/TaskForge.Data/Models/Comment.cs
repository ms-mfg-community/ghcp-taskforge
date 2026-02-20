using System.ComponentModel.DataAnnotations;

namespace TaskForge.Data.Models;

public class Comment
{
    public int Id { get; set; }

    [Required]
    [MaxLength(2000)]
    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int TaskItemId { get; set; }
    public TaskItem TaskItem { get; set; } = null!;

    public string AuthorId { get; set; } = string.Empty;
    public ApplicationUser Author { get; set; } = null!;
}

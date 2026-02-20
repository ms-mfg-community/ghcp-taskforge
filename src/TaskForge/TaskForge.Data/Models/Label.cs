using System.ComponentModel.DataAnnotations;

namespace TaskForge.Data.Models;

public class Label
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(7)]
    public string Color { get; set; } = "#3B82F6";

    public ICollection<TaskLabel> TaskLabels { get; set; } = [];
}

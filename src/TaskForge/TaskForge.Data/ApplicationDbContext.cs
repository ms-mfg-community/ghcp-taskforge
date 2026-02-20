using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskForge.Data.Models;

namespace TaskForge.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Label> Labels => Set<Label>();
    public DbSet<TaskLabel> TaskLabels => Set<TaskLabel>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // TaskLabel composite key
        builder.Entity<TaskLabel>()
            .HasKey(tl => new { tl.TaskItemId, tl.LabelId });

        // Project relationships
        builder.Entity<Project>()
            .HasOne(p => p.CreatedBy)
            .WithMany(u => u.Projects)
            .HasForeignKey(p => p.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        // TaskItem relationships
        builder.Entity<TaskItem>()
            .HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<TaskItem>()
            .HasOne(t => t.AssignedTo)
            .WithMany(u => u.AssignedTasks)
            .HasForeignKey(t => t.AssignedToId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<TaskItem>()
            .HasOne(t => t.CreatedBy)
            .WithMany()
            .HasForeignKey(t => t.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        // Comment relationships
        builder.Entity<Comment>()
            .HasOne(c => c.TaskItem)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TaskItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Comment>()
            .HasOne(c => c.Author)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        // TaskLabel relationships
        builder.Entity<TaskLabel>()
            .HasOne(tl => tl.TaskItem)
            .WithMany(t => t.TaskLabels)
            .HasForeignKey(tl => tl.TaskItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<TaskLabel>()
            .HasOne(tl => tl.Label)
            .WithMany(l => l.TaskLabels)
            .HasForeignKey(tl => tl.LabelId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.Entity<Project>()
            .HasIndex(p => p.Status);

        builder.Entity<TaskItem>()
            .HasIndex(t => t.Status);

        builder.Entity<TaskItem>()
            .HasIndex(t => t.Priority);

        builder.Entity<TaskItem>()
            .HasIndex(t => t.ProjectId);

        builder.Entity<Label>()
            .HasIndex(l => l.Name)
            .IsUnique();
    }
}

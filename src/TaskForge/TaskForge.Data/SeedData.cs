using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskForge.Data.Models;

namespace TaskForge.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        context.Database.EnsureCreated();

        if (context.Labels.Any())
        {
            return;
        }

        context.Labels.AddRange(
            new Label { Name = "Bug", Color = "#EF4444" },
            new Label { Name = "Feature", Color = "#3B82F6" },
            new Label { Name = "Enhancement", Color = "#22C55E" }
        );

        context.SaveChanges();
    }
}

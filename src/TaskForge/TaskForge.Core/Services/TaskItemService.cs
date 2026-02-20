using Microsoft.EntityFrameworkCore;
using TaskForge.Core.DTOs;
using TaskForge.Core.Interfaces;
using TaskForge.Data;
using TaskForge.Data.Models;

namespace TaskForge.Core.Services;

public class TaskItemService : ITaskItemService
{
    private readonly ApplicationDbContext _context;

    public TaskItemService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItemDto>> GetTasksByProjectAsync(int projectId)
    {
        return await _context.TaskItems
            .Include(t => t.Project)
            .Include(t => t.AssignedTo)
            .Where(t => t.ProjectId == projectId)
            .Select(t => MapToDto(t))
            .ToListAsync();
    }

    public async Task<TaskItemDto?> GetTaskByIdAsync(int id)
    {
        var task = await _context.TaskItems
            .Include(t => t.Project)
            .Include(t => t.AssignedTo)
            .FirstOrDefaultAsync(t => t.Id == id);

        return task is null ? null : MapToDto(task);
    }

    public async Task<TaskItemDto> CreateTaskAsync(CreateTaskItemDto dto, string userId)
    {
        var taskItem = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            DueDate = dto.DueDate,
            ProjectId = dto.ProjectId,
            AssignedToId = dto.AssignedToId,
            CreatedById = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.TaskItems.Add(taskItem);
        await _context.SaveChangesAsync();

        var created = await _context.TaskItems
            .Include(t => t.Project)
            .Include(t => t.AssignedTo)
            .FirstAsync(t => t.Id == taskItem.Id);

        return MapToDto(created);
    }

    public async Task<TaskItemDto?> UpdateTaskAsync(int id, UpdateTaskItemDto dto)
    {
        var task = await _context.TaskItems
            .Include(t => t.Project)
            .Include(t => t.AssignedTo)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task is null)
            return null;

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.Priority = dto.Priority;
        task.Status = dto.Status;
        task.DueDate = dto.DueDate;
        task.AssignedToId = dto.AssignedToId;
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return MapToDto(task);
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task is null)
            return false;

        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<TaskItemDto>> GetTasksByAssigneeAsync(string userId)
    {
        return await _context.TaskItems
            .Include(t => t.Project)
            .Include(t => t.AssignedTo)
            .Where(t => t.AssignedToId == userId)
            .Select(t => MapToDto(t))
            .ToListAsync();
    }

    private static TaskItemDto MapToDto(TaskItem task)
    {
        return new TaskItemDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority,
            Status = task.Status,
            DueDate = task.DueDate,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt,
            ProjectId = task.ProjectId,
            ProjectName = task.Project?.Name ?? string.Empty,
            AssignedToId = task.AssignedToId,
            AssigneeName = task.AssignedTo?.DisplayName
        };
    }
}

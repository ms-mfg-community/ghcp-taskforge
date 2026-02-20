using Microsoft.EntityFrameworkCore;
using TaskForge.Core.DTOs;
using TaskForge.Core.Interfaces;
using TaskForge.Data;
using TaskForge.Data.Models;

namespace TaskForge.Core.Services;

public class ProjectService : IProjectService
{
    private readonly ApplicationDbContext _context;

    public ProjectService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
    {
        return await _context.Projects
            .Include(p => p.CreatedBy)
            .Include(p => p.Tasks)
            .Select(p => MapToDto(p))
            .ToListAsync();
    }

    public async Task<ProjectDto?> GetProjectByIdAsync(int id)
    {
        var project = await _context.Projects
            .Include(p => p.CreatedBy)
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id);

        return project is null ? null : MapToDto(project);
    }

    public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto dto, string userId)
    {
        var project = new Project
        {
            Name = dto.Name,
            Description = dto.Description,
            CreatedById = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        // Reload with navigation properties
        var created = await _context.Projects
            .Include(p => p.CreatedBy)
            .Include(p => p.Tasks)
            .FirstAsync(p => p.Id == project.Id);

        return MapToDto(created);
    }

    public async Task<ProjectDto?> UpdateProjectAsync(int id, UpdateProjectDto dto)
    {
        var project = await _context.Projects
            .Include(p => p.CreatedBy)
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project is null)
            return null;

        project.Name = dto.Name;
        project.Description = dto.Description;
        project.Status = dto.Status;
        project.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return MapToDto(project);
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project is null)
            return false;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<ProjectDto>> GetProjectsByUserAsync(string userId)
    {
        return await _context.Projects
            .Include(p => p.CreatedBy)
            .Include(p => p.Tasks)
            .Where(p => p.CreatedById == userId)
            .Select(p => MapToDto(p))
            .ToListAsync();
    }

    private static ProjectDto MapToDto(Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status,
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt,
            CreatedById = project.CreatedById,
            CreatedByName = project.CreatedBy?.DisplayName ?? string.Empty,
            TaskCount = project.Tasks?.Count ?? 0
        };
    }
}

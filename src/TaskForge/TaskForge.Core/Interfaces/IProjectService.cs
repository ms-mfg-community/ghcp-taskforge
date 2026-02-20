using TaskForge.Core.DTOs;

namespace TaskForge.Core.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
    Task<ProjectDto?> GetProjectByIdAsync(int id);
    Task<ProjectDto> CreateProjectAsync(CreateProjectDto dto, string userId);
    Task<ProjectDto?> UpdateProjectAsync(int id, UpdateProjectDto dto);
    Task<bool> DeleteProjectAsync(int id);
    Task<IEnumerable<ProjectDto>> GetProjectsByUserAsync(string userId);
}

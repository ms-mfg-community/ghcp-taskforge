using TaskForge.Core.DTOs;

namespace TaskForge.Core.Interfaces;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItemDto>> GetTasksByProjectAsync(int projectId);
    Task<TaskItemDto?> GetTaskByIdAsync(int id);
    Task<TaskItemDto> CreateTaskAsync(CreateTaskItemDto dto, string userId);
    Task<TaskItemDto?> UpdateTaskAsync(int id, UpdateTaskItemDto dto);
    Task<bool> DeleteTaskAsync(int id);
    Task<IEnumerable<TaskItemDto>> GetTasksByAssigneeAsync(string userId);
}

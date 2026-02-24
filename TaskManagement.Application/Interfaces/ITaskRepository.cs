
using TaskManagement.Application.Dtos.Responses;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Interfaces;

public interface ITaskRepository
{
    Task AddAsync(TaskItem task);
    Task<IEnumerable<TaskItem>> GetAllAsync(ETaskStatus? status);
    Task<TaskItem?> GetByIdAsync(Guid id);

    Task<List<TaskResponseDto>> GetTasksByUserAsync(Guid userId, string? status = null);
}
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Dtos.Responses;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;
public class TaskService(ITaskRepository _taskRepository, IUserRepository _userRepository)
{
    public async Task<Guid> CreateAsync(CreateTaskDto dto)
    {
        User user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user is null)
            throw new ArgumentException("User does not exist");

        TaskItem task = new TaskItem(dto.Title, dto.Description, dto.UserId);

        await _taskRepository.AddAsync(task);
        await _taskRepository.SaveChangesAsync();

        return task.Id;
    }

    public async Task<IEnumerable<TaskResponseDto>> GetAllAsync(ETaskStatus? status)
    {
        IEnumerable<TaskItem> tasks = await _taskRepository.GetAllAsync(status);

        return tasks.Select(t => new TaskResponseDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status.ToString(),
            UserId = t.UserId
        });
    }

    public async Task ChangeStatusAsync(Guid id, ETaskStatus status)
    {
        TaskItem task = await _taskRepository.GetByIdAsync(id)
            ?? throw new ArgumentException("Task not found");

        task.ChangeStatus(status);

        await _taskRepository.SaveChangesAsync();
    }
}
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;
public class TaskService(ITaskRepository _taskRepository, IUserRepository _userRepository)
{
    public async Task<Guid> CreateAsync(CreateTaskDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user is null)
            throw new ArgumentException("User does not exist");

        var task = new TaskItem(dto.Title, dto.Description, dto.UserId);

        await _taskRepository.AddAsync(task);
        await _taskRepository.SaveChangesAsync();

        return task.Id;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync(ETaskStatus? status)
        => await _taskRepository.GetAllAsync(status);

    public async Task ChangeStatusAsync(Guid id, ETaskStatus status)
    {
        var task = await _taskRepository.GetByIdAsync(id)
            ?? throw new ArgumentException("Task not found");

        task.ChangeStatus(status);

        await _taskRepository.SaveChangesAsync();
    }
}
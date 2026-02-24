using TaskManagement.Application.Dtos;
using TaskManagement.Application.Dtos.Responses;
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;
public class TaskService(IUnitOfWork _unitOfWork)
{
    public async Task<Guid> CreateAsync(CreateTaskDto dto)
    {
        User user = await _unitOfWork.Users.GetByIdAsync(dto.UserId);
        if (user is null)
            throw new NotFoundException("User does not exist");

        TaskItem task = new TaskItem(dto.Title, dto.Description, dto.UserId);

        await _unitOfWork.Tasks.AddAsync(task);
        await _unitOfWork.SaveChangesAsync();

        return task.Id;
    }

    public async Task<IEnumerable<TaskResponseDto>> GetAllAsync(ETaskStatus? status)
    {
        IEnumerable<TaskItem> tasks = await _unitOfWork.Tasks.GetAllAsync(status);

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
        TaskItem task = await _unitOfWork.Tasks.GetByIdAsync(id)
            ?? throw new NotFoundException("Task not found");

        task.ChangeStatus(status);

        await _unitOfWork.SaveChangesAsync();
    }
}
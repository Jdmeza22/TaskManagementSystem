
namespace TaskManagement.Application.Dtos;

public record CreateTaskDto(string Title, string? Description, Guid UserId);
namespace TaskManagement.Application.Dtos.Responses;

public class TaskResponseDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string Status { get; init; } = default!;
    public Guid UserId { get; init; }
}
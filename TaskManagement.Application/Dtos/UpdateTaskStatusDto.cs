using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Dtos;

public record UpdateTaskStatusDto(ETaskStatus Status);

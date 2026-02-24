using TaskManagement.Domain.Enums;
namespace TaskManagement.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public ETaskStatus Status { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private TaskItem() { }

    public TaskItem(string title, string description, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required");

        if (userId == Guid.Empty)
            throw new ArgumentException("User is required");

        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        UserId = userId;
        Status = ETaskStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(ETaskStatus newStatus)
    {
        if (Status == ETaskStatus.Pending && newStatus == ETaskStatus.Done)
            throw new InvalidOperationException("Cannot change directly from Pending to Done");

        Status = newStatus;
    }
}
namespace TaskManagement.Application.Interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    ITaskRepository Tasks { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
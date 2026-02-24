using TaskManagement.Application.Interfaces;

namespace TaskManagement.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IUserRepository Users { get; }
    public ITaskRepository Tasks { get; }

    public UnitOfWork(
        AppDbContext context,
        IUserRepository userRepository,
        ITaskRepository taskRepository)
    {
        _context = context;
        Users = userRepository;
        Tasks = taskRepository;
    }

    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
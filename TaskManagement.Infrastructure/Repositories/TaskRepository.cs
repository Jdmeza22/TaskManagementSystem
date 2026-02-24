using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;
using TaskManagement.Infrastructure.Persistence;

public class TaskRepository(InMemoryDbContext _context) : ITaskRepository
{
    
    public async Task AddAsync(TaskItem task)
        => await _context.Tasks.AddAsync(task);

    public async Task<IEnumerable<TaskItem>> GetAllAsync(ETaskStatus? status)
    {
        var query = _context.Tasks.AsQueryable();

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        return await query.ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
        => await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}
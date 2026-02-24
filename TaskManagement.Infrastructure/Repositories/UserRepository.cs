
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Persistence;
namespace TaskManagement.Infrastructure.Repositories;

public class UserRepository(InMemoryDbContext _context)  : IUserRepository
{
    public async Task AddAsync(User user)
        => await _context.Users.AddAsync(user);

    public async Task<IEnumerable<User>> GetAllAsync()
        => await _context.Users.ToListAsync();

    public async Task<User?> GetByIdAsync(Guid id)
        => await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}
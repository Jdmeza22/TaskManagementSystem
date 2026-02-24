

using Microsoft.EntityFrameworkCore;
using System;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistence;

public class InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : DbContext( options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

}

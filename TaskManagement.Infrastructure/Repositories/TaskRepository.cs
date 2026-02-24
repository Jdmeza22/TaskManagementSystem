using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using TaskManagement.Application.Dtos.Responses;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;
using TaskManagement.Infrastructure.Persistence;

public class TaskRepository(AppDbContext _context, IConfiguration _configuration) : ITaskRepository
{
    private string connectionString = _configuration.GetConnectionString("DefaultConnection");

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

    public async Task<List<TaskResponseDto>> GetTasksByUserAsync(Guid userId, string status)
    {
        var tasks = new List<TaskResponseDto>();
        var connectionString = _configuration.GetConnectionString("DefaultConnection");

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand("GetUserTasks", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Value = userId });
        command.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 25) { Value = status });

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            tasks.Add(new TaskResponseDto
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                UserId = reader.GetGuid(reader.GetOrdinal("UserId")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
            });
        }

        return tasks;
    }

}
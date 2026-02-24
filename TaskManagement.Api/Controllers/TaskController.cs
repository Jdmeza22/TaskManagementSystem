using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Dtos;
using TaskManagement.Domain.Enums;

[ApiController]
[Route("api/tasks")]
public class TasksController(TaskService _service) : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ETaskStatus? status)
        => Ok(await _service.GetAllAsync(status));

    [HttpPut("{id}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, UpdateTaskStatusDto dto)
    {
        await _service.ChangeStatusAsync(id, dto.Status);
        return NoContent();
    }
}
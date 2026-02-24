using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Dtos.Responses;
using TaskManagement.Domain.Enums;

[ApiController]
[Route("api/tasks")]
public class TasksController(TaskService _service) : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto dto)
    {
        Guid id = await _service.CreateAsync(dto);
        return Ok(id);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetAll(
    [FromQuery] ETaskStatus? status)
    {
        var result = await _service.GetAllAsync(status);
        return Ok(result);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, UpdateTaskStatusDto dto)
    {
        await _service.ChangeStatusAsync(id, dto.Status);
        return NoContent();
    }
}
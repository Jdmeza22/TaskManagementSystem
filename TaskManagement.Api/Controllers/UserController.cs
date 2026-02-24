using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Dtos;

[ApiController]
[Route("api/users")]
public class UsersController(UserService _service) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        Guid id = await _service.CreateAsync(dto);
        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());
}
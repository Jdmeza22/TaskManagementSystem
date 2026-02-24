using TaskManagement.Application.Dtos;
using TaskManagement.Application.Dtos.Responses;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;

public class UserService(IUnitOfWork _unitOfWork)
{
    public async Task<Guid> CreateAsync(CreateUserDto dto)
    {
        User user = new User(dto.Name, dto.Email);

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return user.Id;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        IEnumerable<User> users = await _unitOfWork.Users.GetAllAsync();

        return users.Select(u => new UserResponseDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email
        });
    }
}
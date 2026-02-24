using TaskManagement.Application.Dtos;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;

public class UserService(IUserRepository _repository)
{
    public async Task<Guid> CreateAsync(CreateUserDto dto)
    {
        var user = new User(dto.Name, dto.Email);

        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();

        return user.Id;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
        => await _repository.GetAllAsync();
}
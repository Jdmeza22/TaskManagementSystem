
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    private User() { }

    public User(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Name is required");

        if (string.IsNullOrWhiteSpace(email))
            throw new ValidationException("Email is required");

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
    }
}
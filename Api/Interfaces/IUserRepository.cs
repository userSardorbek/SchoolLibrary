using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Model;

namespace WebApplication1.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();

    Task<User?> GetUserByIdAsync(Guid userId);

    Task<User> CreateAsync(CreateUserDto userDto);
}
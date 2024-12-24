using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Mapper;
using WebApplication1.Model;

namespace WebApplication1.Service;

public class UserService
{

    private readonly ApplicationDbContext _context;

    // public UserService(ApplicationDbContext context)
    // {
    //     _context = context;
    // }
    //
    // public async Task<List<User>> GetAllUsersAsync()
    // {
    //     return await _context.User.ToListAsync();
    // }
    //
    // public async Task<User?> GetUserByIdAsync(Guid id)
    // { 
    //     var user = await _context.User.FirstOrDefaultAsync(s => s.UserId == id);
    //     return user;
    // }
    //
    // public async Task<User> CreateAsync(CreateUserDto userDto)
    // {
    //     var userModel = userDto.FromCreateUserDtoToUser();
    //     await _context.User.AddAsync(userModel);
    //     await _context.SaveChangesAsync();
    //     return userModel;
    // }
}
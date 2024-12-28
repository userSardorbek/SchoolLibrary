using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Model;

namespace WebApplication1.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
    }
}
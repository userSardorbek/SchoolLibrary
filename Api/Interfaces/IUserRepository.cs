using WebApplication1.Model;

namespace WebApplication1.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByUsername(string username);
}
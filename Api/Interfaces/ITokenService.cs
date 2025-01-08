using WebApplication1.Model;

namespace WebApplication1.Interfaces;

public interface ITokenService
{
    string CreateToken(User user, IList<string> roles);
}
using WebApplication1.Dto;
using WebApplication1.Model;

namespace WebApplication1.Mapper;

public static class UserMapper
{
    public static ReturnUserDto FromUserToReturnUserDto(this User user)
    {
        return new ReturnUserDto()
        {
            Username = user.Username,
            Email = user.Email,
            FullName = user.FullName
        };
    }

    public static User FromCreateUserDtoToUser(this CreateUserDto createDto)
    {
        return new User()
        {
            UserId = Guid.NewGuid(),
            Username = createDto.Username,
            Email = createDto.Email,
            FullName = createDto.FullName,
            Password = createDto.Password
        };
    }
}
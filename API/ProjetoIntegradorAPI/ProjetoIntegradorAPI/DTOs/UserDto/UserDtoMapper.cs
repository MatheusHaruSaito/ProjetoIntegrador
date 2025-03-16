using ProjetoIntegradorAPI.Models;

namespace ProjetoIntegradorAPI.DTOs.UserDto
{
    public static class UserDtoMapper
    {
        public static User PostUserDtoToUser(this PostUserDto postUserDto)
        {
            User user = new()
            {
                Type = postUserDto.Type,
                Name = postUserDto.Name,
                Password = postUserDto.Password,
                Email = postUserDto.Email,
                IsActive = postUserDto.IsActive,
            };
            return user;
        }
    }
}

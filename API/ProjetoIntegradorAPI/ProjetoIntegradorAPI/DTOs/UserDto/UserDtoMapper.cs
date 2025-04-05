using ProjetoIntegradorAPI.Models;

namespace ProjetoIntegradorAPI.DTOs.UserDto
{
    public static class UserDtoMapper
    {
        public static User PostUserDtoToUser(this PostUserDto postUserDto)
        {
            User user = new()
            {
                Name = postUserDto.Name,
                Password = postUserDto.Password,
                Email = postUserDto.Email,
            };
            return user;
        }
        public static User PutUserDtoToUser(this PutUserDto putUserDto)
        {
            User user = new()
            {
                Role = putUserDto.Role,
                Name = putUserDto.Name,
                Password = putUserDto.Password,
                Email = putUserDto.Email,
            };
            return user;
        }
    }
}

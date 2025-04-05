using ProjetoIntegradorAPI.Models;

namespace ProjetoIntegradorAPI.DTOs.UserDto
{
    public class PutUserDto
    {
        public UserRoleEnum Role { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}

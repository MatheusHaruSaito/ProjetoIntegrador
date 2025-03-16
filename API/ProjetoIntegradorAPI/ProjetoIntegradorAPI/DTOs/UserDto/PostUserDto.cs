using ProjetoIntegradorAPI.Models;

namespace ProjetoIntegradorAPI.DTOs.UserDto
{
    public class PostUserDto
    {
        public UserTypeEnum Type { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}

using ProjetoIntegradorAPI.Models;

namespace ProjetoIntegradorAPI.DTOs.UserDto
{
    public class PostUserDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Cep { get; set; }
        public string Description { get; set; }

    }
}

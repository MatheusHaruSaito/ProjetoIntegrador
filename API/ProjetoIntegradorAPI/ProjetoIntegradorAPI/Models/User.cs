namespace ProjetoIntegradorAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public UserRoleEnum Role { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}

namespace ProjetoIntegradorAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public UserRoleEnum Role { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}

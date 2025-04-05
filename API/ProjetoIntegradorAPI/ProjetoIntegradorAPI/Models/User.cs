namespace ProjetoIntegradorAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public UserTypeEnum Type { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime updateDate { get; set; }

    }
}

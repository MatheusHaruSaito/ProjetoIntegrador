namespace ProjetoIntegradorAPI.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public UserTypeEnum Type { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime creationDate { get; set; } = DateTime.Now;
        public DateTime updateDate { get; set; } = DateTime.Now;

    }
}

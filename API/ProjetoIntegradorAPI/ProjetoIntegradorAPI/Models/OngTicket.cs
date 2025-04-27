namespace ProjetoIntegradorAPI.Models
{
    public class OngTicket
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool Reviwed { get; set; }
        public bool Accpeted { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public string Cnpj { get; set; }
        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddYears(1);
    }
}

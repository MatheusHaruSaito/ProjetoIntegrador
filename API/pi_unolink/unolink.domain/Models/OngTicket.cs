using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace unolink.domain.Models
{
    public class OngTicket : BaseModel
    {
        public bool Reviwed { get; private set; }
        public bool Accepeted { get; private set; }
        public string Description { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        [MaxLength(8)]
        public string Cep { get; private set; }
        [MaxLength(14)]
        public string Cnpj { get; private set; }
        public DateTime ExpirationDate { get; private set; } = DateTime.UtcNow.AddYears(1);
        public void AcceptedOng()
        {
            Reviwed = true;
            Accepeted = true;
        }
        public void ReviwedOng()
        {
            Accepeted = false;
            Reviwed = true;
        }
        public OngTicket(string description, string name, string email, string cep, string cnpj)
        {
            Reviwed = false;
            Accepeted = false;
            Description = description;
            Name = name;
            Email = email;
            Cep = cep;
            Cnpj = cnpj;
        }
    }
}
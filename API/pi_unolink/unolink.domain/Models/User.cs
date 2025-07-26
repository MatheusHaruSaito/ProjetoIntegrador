using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace unolink.domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public string Description { get; private set; }
        public string Cep { get; private set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User() { }
        public User( string name, string email, string password, string description, string cep)
        {
            Id = Guid.NewGuid();
            UserName = name;
            Email = email;
            PasswordHash = password;
            Description = description;
            Cep = cep;
            IsActive = true;
        }
        public void Update( string name, string email, string password)
        {
            UserName = name;
            Email = email;
            PasswordHash = password;
        }
    }
}

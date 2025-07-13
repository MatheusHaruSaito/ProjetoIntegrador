using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace unolink.domain.Models
{
    public class User : BaseModel
    {
        public UserRoleEnum Role { get; private set; }
        public string Name { get; private set; }
        public string Password { get; set; }
        public string Email { get; private set; }
        public string Description { get; private set; }
        public string Cep { get; private set; }
        public User(UserRoleEnum role, string name, string email, string password, string description, string cep)
        {
            Id = Guid.NewGuid();
            Role = role;
            Name = name;
            Email = email;
            Password = password;
            Description = description;
            Cep = cep;
            IsActive = true;
        }
        public void Update(UserRoleEnum role, string name, string email, string password)
        {
            Role = role;
            Name = name;
            Email = email;
            Password = password;
        }
    }
}

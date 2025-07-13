using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unolink.domain.Models;

namespace unolink.api.Application.Models.Dtos
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public UserRoleEnum Role { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Cep { get; set; }
        public bool IsActive { get; set; }
        public string CreationDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unolink.domain.Models;

namespace unolink.api.Application.Models.Request
{
    public class UpdateUserRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ProfileImgPath { get; set; }
        public IFormFile? ProfileImg { get; set; }
    }
}
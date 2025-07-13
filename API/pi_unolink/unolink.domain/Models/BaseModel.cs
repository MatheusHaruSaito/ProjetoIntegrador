using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unolink.domain.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; } 
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
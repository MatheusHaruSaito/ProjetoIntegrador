using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unolink.domain.Models
{
    public class PostVotes
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid PostId { get; set; }
        public UserPost Post { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

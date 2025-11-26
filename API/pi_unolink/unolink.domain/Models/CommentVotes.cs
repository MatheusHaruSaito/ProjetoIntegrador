using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unolink.domain.Models
{
    public class CommentVotes
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid CommentId { get; set; }
        public PostComment Comment { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}

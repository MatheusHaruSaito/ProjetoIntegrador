using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unolink.domain.Models
{
    public class PostComment : BaseModel
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public string Text { get; set; }
        public int Vote { get; set; }
        public PostComment(Guid postId, Guid userId, string text)
        {
            PostId = postId;
            Id = Guid.NewGuid();
            UserId = userId;
            Text = text;
            Vote = 0;
        }
    }
}

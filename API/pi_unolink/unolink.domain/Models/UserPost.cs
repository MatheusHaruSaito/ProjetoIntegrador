using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unolink.domain.Models
{
    public class UserPost : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public int Votes { get; set; }
        public DateTime UpdateTime { get; set; }
        public string? PostImgPath { get; set; } = string.Empty;
        public List<PostComment> Comments { get; set; } = new();

        public UserPost() { }

        public UserPost(string title,string description, Guid userId)
        {
            Id = Guid.NewGuid();
            Title = title;
            UserId = userId;
            UpdateTime = CreatedAt;
            Description = description;
        }
        public void Update(string title,string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            UpdateTime = DateTime.UtcNow;
        }
    }
}

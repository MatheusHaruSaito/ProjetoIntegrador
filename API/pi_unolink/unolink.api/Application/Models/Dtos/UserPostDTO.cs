using unolink.domain.Models;

namespace unolink.api.Application.Models.Dtos
{
    public class UserPostDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Votes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateTime { get; set; }
        public string? PostImgPath { get; set; }
        public IEnumerable<PostCommentDTO> Comments { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string? ProfileImgPath { get; set; }
    }
}

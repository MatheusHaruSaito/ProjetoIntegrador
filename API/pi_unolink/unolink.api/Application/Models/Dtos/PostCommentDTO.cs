namespace unolink.api.Application.Models.Dtos
{
    public class PostCommentDTO
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public int Vote { get; set; }
        public string UserUsername { get; set; }
        public string userProfileImgPath { get; set; }

    }
}

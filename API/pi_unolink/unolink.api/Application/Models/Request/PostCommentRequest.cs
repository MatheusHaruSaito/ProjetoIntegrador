namespace unolink.api.Application.Models.Request
{
    public class PostCommentRequest
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
    }
}

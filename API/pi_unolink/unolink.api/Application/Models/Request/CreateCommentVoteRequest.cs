namespace unolink.api.Application.Models.Request
{
    public class CreateCommentVoteRequest
    {
        public Guid UserId { get; set; }
        public Guid CommentId { get; set; }
    }
}

namespace unolink.api.Application.Models.Request
{
    public class CreatePostRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public IFormFile? PostImg { get; set; }
    }
}

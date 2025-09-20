namespace unolink.api.Application.Models.Request
{
    public class UpdateUserPostRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Votes { get; set; }
        public DateTime UpdateTime { get; set; }
        public string? PostImgPath { get; set; }
        public IFormFile? PostImg { get; set; }


    }
}

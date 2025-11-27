namespace unolink.api.Application.Models.Request
{
    public class UserProfileEditRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ProfileImgPath { get; set; }
        public IFormFile? ProfileImg { get; set; }
    }
}
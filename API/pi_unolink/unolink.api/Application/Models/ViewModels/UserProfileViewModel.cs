using unolink.api.Application.Models.Dtos;

namespace unolink.api.Application.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public ICollection<UserPostSummaryDTO> UserPosts { get; set; }
        public string Cep { get; set; }
        public bool IsActive { get; set; }
        public string CreationDate { get; set; }
        public string ProfileImgPath { get; set; }
    }
}

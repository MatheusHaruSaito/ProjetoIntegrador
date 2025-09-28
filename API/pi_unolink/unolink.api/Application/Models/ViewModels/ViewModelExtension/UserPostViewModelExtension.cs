using unolink.api.Application.Models.Dtos;

namespace unolink.api.Application.Models.ViewModels.ViewModelExtension
{
    public static class UserPostViewModelExtension
    {
        public static UserPostViewModel ToViewModel(this UserPostDTO userPostDTO)
        {
            return new UserPostViewModel {
                Id = userPostDTO.Id,
                Title = userPostDTO.Title,
                Description = userPostDTO.Description,
                UserId = userPostDTO.UserId,
                Votes = userPostDTO.Votes,
                CreatedAt = userPostDTO.CreatedAt,
                UpdateTime = userPostDTO.UpdateTime,
                PostImgPath = userPostDTO.PostImgPath,
                Comments = userPostDTO.Comments
            };
        }
    }
}

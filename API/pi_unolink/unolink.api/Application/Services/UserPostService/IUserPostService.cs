using unolink.api.Application.Models.Dtos;
using unolink.api.Application.Models.Request;
using unolink.domain.Core.Interfaces;
using unolink.domain.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace unolink.api.Application.Services.UserPostService
{
    public interface IUserPostService
    {
        Task<List<UserPostDTO>> GetAll();
        Task<bool> UseTriggerActive(Guid id);
        Task<bool> Add(CreatePostRequest request,string baseUrl);
        Task<UserPostDTO> GetById(Guid id);
        Task<bool> Update(UpdateUserPostRequest request, string baseUrl);
        Task<bool> Comment(PostCommentRequest request);
        Task<bool> VoteComment(CreateCommentVoteRequest request);
        Task<bool> Vote(CreateVoteRequest request);

    }
}

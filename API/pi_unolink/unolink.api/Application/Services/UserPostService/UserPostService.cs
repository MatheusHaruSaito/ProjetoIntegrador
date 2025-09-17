using Azure.Core;
using Microsoft.AspNetCore.Identity;
using unolink.api.Application.Models.Dtos;
using unolink.api.Application.Models.Request;
using unolink.api.Application.Services.ImagesSevice;
using unolink.domain.Core.Interfaces;
using unolink.domain.Models;
using unolink.infrastructure.Repositories;

namespace unolink.api.Application.Services.UserPostService
{
    public class UserPostService : IUserPostService
    {
        private readonly IUserPostRepository _userPostRepostiory;
        private readonly IFilesService _fileService;

        public UserPostService(IUserPostRepository userPostRepostiory, IFilesService fileService)
        {
            _userPostRepostiory = userPostRepostiory;
            _fileService = fileService;
        }
        public async Task<bool> Add(CreatePostRequest request, string baseUrl)
        {
            UserPost post = new(request.Title, request.Description, request.UserId);
            post.PostImgPath = await _fileService.AddImage(request.PostImg, baseUrl);
            _userPostRepostiory.Add(post);
            return await _userPostRepostiory.UnitOfWork.SaveEntitiesAsync();
        }

        public async Task<List<UserPostDTO>> GetAll()
        {
            var data= await _userPostRepostiory.GetAll();
            if(data is null)
            {
                return null;
            }
            var userPostDTO = data.Select(x => new UserPostDTO
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                UserId = x.UserId,
                Votes = x.Votes,
                CreatedAt = x.CreatedAt,
                UpdateTime = x.UpdateTime,
                PostImgPath = x.PostImgPath
            }).ToList();
            return userPostDTO;
        }

        public async Task<UserPostDTO> GetById(Guid id)
        {
            var userPost = await _userPostRepostiory.GetById(id);
            if(userPost is null)
            {
                return null;
            }
            var userPostDTO = new UserPostDTO
            {
                Id = userPost.Id,
                Title = userPost.Title,
                Description = userPost.Description,
                UserId = userPost.UserId,
                Votes = userPost.Votes,
                CreatedAt = userPost.CreatedAt,
                UpdateTime = userPost.UpdateTime,
                PostImgPath = userPost.PostImgPath
            };
            return userPostDTO;
        }

        public async Task<bool> UseTriggerActive(Guid id)
        {
            var result = await _userPostRepostiory.UseTriggerActive(id);
            if (!result) return false;

            await _userPostRepostiory.UnitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(UpdateUserPostRequest request, string baseUrl)
        {
            request.PostImgPath = await _fileService.AddImage(request.PostImg, baseUrl);

            var userPost = await _userPostRepostiory.GetById(request.Id);

            if (userPost is null) return false;
            await _fileService.DeleteFile(userPost.PostImgPath);
            userPost.Update(request.Title,request.Description);
            userPost.UpdateTime = DateTime.UtcNow;
            return await _userPostRepostiory.UnitOfWork.SaveEntitiesAsync();
        }
    }
}

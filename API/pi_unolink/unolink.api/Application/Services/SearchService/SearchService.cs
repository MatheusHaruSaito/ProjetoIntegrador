using Microsoft.AspNetCore.Identity;
using unolink.api.Application.Models.Dtos;
using unolink.domain.Core.Interfaces;
using unolink.domain.Enums;
using unolink.domain.Models;
using unolink.domain.ValueObjects;
using unolink.infrastructure.Repositories;

namespace unolink.api.Application.Services.SearchService
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IUserPostRepository _userPostRepository;
        private readonly UserManager<User> _userManager;
        public SearchService(ISearchRepository searchRepository, UserManager<User> userManager, IUserPostRepository userPostRepository)
        {
            _userManager = userManager;
            _searchRepository = searchRepository;
            _userPostRepository = userPostRepository;
        }
        public async Task<SearchResultDTO> SearchAsync(SearchType type, string q, int page, int pageSize)
        {
            var pagination = new PaginationData(page, pageSize);
            var data = _searchRepository.SearchAsync(type, q, pagination);

            if(data is null)
            {
                return null;
            }

            var postsId = data.Result.Posts.Select(x => x.Id).ToList();
            var votesCounts = await _userPostRepository.GetVotesCountByPostIdsAsync(postsId);
            var votesDict = votesCounts.ToDictionary(vc => vc.PostId, vc => vc.Count);
            SearchResultDTO searchResultDTO = new()
            {
                User = data.Result.User.Select(u => new UserDTO
                {
                    Id = u.Id,
                    Role = _userManager.GetRolesAsync(u).Result,
                    Name = u.UserName,
                    Email = u.Email,
                    Password = u.PasswordHash,
                    Description = u.Description,
                    Cep = u.Cep,
                    IsActive = u.IsActive,
                    CreationDate = u.CreatedAt.ToString("dd-MM-yyyy"),
                    ProfileImgPath = u.ProfileImgPath,
                }),
                Posts = data.Result.Posts.Select(p => new UserPostDTO 
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    UserId = p.UserId,
                    Votes = votesDict.TryGetValue(p.Id, out var count) ? count : 0,
                    CreatedAt = p.CreatedAt,
                    UpdateTime = p.UpdateTime,
                    PostImgPath = p.PostImgPath,
                    ProfileImgPath = p.User.ProfileImgPath,
                    UserName = p.User.UserName,
                }),
                paginationData = pagination
            };
            return searchResultDTO;
        }
    }
}

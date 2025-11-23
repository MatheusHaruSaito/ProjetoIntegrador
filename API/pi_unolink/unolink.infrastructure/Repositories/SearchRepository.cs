using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unolink.domain.Core.Interfaces;
using unolink.domain.Enums;
using unolink.domain.Models;
using unolink.domain.ValueObjects;
using unolink.infrastructure.Context;

namespace unolink.infrastructure.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ApplicationDataContext _context;
        public SearchRepository(ApplicationDataContext context)
        {
            _context = context;
        }
        public async Task<(List<User> User, List<UserPost> Posts, PaginationData pagination)> SearchAsync(SearchType type, string q, PaginationData paginationData)
        {
            List<User> userResults = new List<User>();
            List<UserPost> postsResults = new List<UserPost>();

            switch (type)
            {
                case SearchType.Users:
                    userResults = await SearchUsersAsync(q, paginationData);
                    break;
                case SearchType.Posts:
                    postsResults = await SearchPostsAsync(q, paginationData);
                    break;
                case SearchType.All:
                    userResults = await SearchUsersAsync(q, paginationData);
                    postsResults = await SearchPostsAsync(q, paginationData);
                    break;
            }

            return (userResults,postsResults,paginationData);
        }

        private async Task<List<User>> SearchUsersAsync(string q, PaginationData pagination)
        {
            return await _context.Users
                .Where(u => u.UserName.Contains(q) || u.Email.Contains(q))
                .OrderBy(u => u.UserName)
                .Skip(pagination.Offset)
                .Take(pagination.PageSize)
                .ToListAsync();
        }
        private async Task<List<UserPost>> SearchPostsAsync(string q, PaginationData pagination)
        {
            return await _context.UserPost
                .Where(p => p.Title.Contains(q) || p.Description.Contains(q))
                .OrderBy(p => p.CreatedAt)
                .Include(p => p.User)
                .Skip(pagination.Offset)
                .Take(pagination.PageSize)
                .ToListAsync();
        }
    }
}

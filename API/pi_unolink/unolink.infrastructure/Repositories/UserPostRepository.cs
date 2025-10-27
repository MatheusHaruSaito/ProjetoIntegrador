using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unolink.domain.Core.Interfaces;
using unolink.domain.Models;
using unolink.infrastructure.Context;

namespace unolink.infrastructure.Repositories
{
    public class UserPostRepository(IUnitOfWork unitOfWork, ApplicationDataContext context) : IUserPostRepository
    {
        private readonly ApplicationDataContext _context = context;
        private readonly DbSet<UserPost> _entity = context.UserPost;
        private readonly DbSet<User> _entityUser = context.Users;
        private readonly DbSet<PostVotes> _entityVotes = context.PostVotes;



        public IUnitOfWork UnitOfWork => unitOfWork;

        public void Add(UserPost entity)
        {
            _entity.Add(entity);
        }

        public async Task<List<UserPost>> GetAll()
        {
            return await _entity
                .Include(p => p.Comments)
                .ToListAsync();

        }

        public async Task<UserPost> GetById(Guid id)
        {
            return await _entity.Include(p => p.Comments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UseTriggerActive(Guid id)
        {
            var post = await _entity.FirstOrDefaultAsync(x => x.Id == id);
            if (post is null) return false;

            post.IsActive = false;
            return true;

        }

        public void Update(UserPost entity)
        {
            _entity.Update(entity);
        }

        public async Task<PostComment> Comment(Guid postId,Guid userId, string text)
        {
            var post = await _entity
                .FirstOrDefaultAsync(x => x.Id == postId);


            if (post is null)
            {
                return null;
            }
            var comment = new PostComment(post.Id,userId,text);
            _context.PostComment.Add(comment);
            post.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> Vote(Guid postId, Guid userId)
        {
            var data = await _entityVotes.FindAsync(userId,postId);
            if(data is not null)
            {
                _entityVotes.Remove(data);
                await unitOfWork.SaveChangesAsync();
                return false;
            }

            var post = await _entity.FirstOrDefaultAsync(x => x.Id == postId);
            var user = await _entityUser.FirstOrDefaultAsync(x => x.Id == userId);

            if(post is null || user is null)
            {
                return false;
            }
            var vote = new PostVotes
            {
                Post = post,
                User = user,
                CreatedAt = DateTime.UtcNow
            };
            _entityVotes.Add(vote);

            await unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<int> VoteCount(Guid postId)
        {
            return await _entityVotes.CountAsync(x => x.PostId == postId);
        }

        public async Task<List<(Guid PostId, int Count)>> GetVotesCountByPostIdsAsync(List<Guid> postIds)
        {
            var result = await _context.PostVotes
                .Where(v => postIds.Contains(v.PostId))
                .GroupBy(v => v.PostId)
                .Select(g => new { PostId = g.Key, Count = g.Count() })
                .ToListAsync();

            return result.Select(x => (x.PostId, x.Count)).ToList();
        }
    }
}

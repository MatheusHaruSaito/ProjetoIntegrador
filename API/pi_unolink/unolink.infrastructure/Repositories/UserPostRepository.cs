using Microsoft.EntityFrameworkCore;
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
        public IUnitOfWork UnitOfWork => unitOfWork;

        public void Add(UserPost entity)
        {
            _entity.Add(entity);
        }

        public async Task<List<UserPost>> GetAll()
        {
            return await _entity.ToListAsync();
        }

        public async Task<UserPost> GetById(Guid id)
        {
            return await _entity.FirstOrDefaultAsync(x => x.Id == id);
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
    }
}

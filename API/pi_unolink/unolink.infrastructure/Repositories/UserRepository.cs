using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unolink.domain.Core.Interfaces;
using unolink.domain.Models;
using unolink.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace unolink.infrastructure.Repositories
{
    public class UserRepository(IUnitOfWork unitOfWork, ApplicationDataContext context) : IRepository<User, Guid>, IUserRepository
    {
        private readonly ApplicationDataContext _context = context;
        private readonly DbSet<User> _entity = context.Set<User>();
        public IUnitOfWork UnitOfWork => unitOfWork; 

        public void Add(User entity)
        {
            _entity.Add(entity);
        }

        public async Task<List<User>> GetAll()
        {
            return await _entity.ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _entity.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _entity.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(User entity)
        {
            _entity.Update(entity);
        }

        public async Task<bool> UseTriggerActive(Guid id)
        {
            var user = await _entity.FirstOrDefaultAsync(x => x.Id == id);

            if (user is null) return false;

            user.IsActive = !user.IsActive;

            return true;
        }
    }
}
﻿using ProjetoIntegradorAPI.Context;
using ProjetoIntegradorAPI.Models;

namespace ProjetoIntegradorAPI.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
        {
            
        }

        public async Task<bool> UserTriggerActive(Guid Id)
        {
            User user = await _applicationDataContext.User.FindAsync(Id);
            if(user == null)
            {
                return false;
            }
            user.IsActive = !user.IsActive;
            user.UpdateDate = DateTime.UtcNow;

            _applicationDataContext.Update(user);

            await _applicationDataContext.SaveChangesAsync();
            return user.IsActive;
        }
        public override async Task<User> AddAsync(User user)
        {
            user.Id = Guid.NewGuid();
            user.CreationDate = DateTime.UtcNow;
            user.UpdateDate = user.CreationDate;
            user.IsActive = true;

            return await base.AddAsync(user);
        }
        public override Task<User> Update(User UpdateEntity)
        {
            UpdateEntity.UpdateDate = DateTime.UtcNow;
            return base.Update(UpdateEntity);
        }
    }
}

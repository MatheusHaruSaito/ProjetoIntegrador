using ProjetoIntegradorAPI.Context;
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
            user.updateDate = DateTime.UtcNow;

            _applicationDataContext.Update(user);

            await _applicationDataContext.SaveChangesAsync();
            return user.IsActive;
        }
        public override async Task<User> AddAsync(User user)
        {
            user.Id = Guid.NewGuid();
            user.creationDate = DateTime.UtcNow;
            user.updateDate = user.creationDate;
            user.IsActive = true;

            await _applicationDataContext.User.AddAsync(user);

            if (await _applicationDataContext.SaveChangesAsync() > 0)
            {
                return null;
            }

            return user;
        }
    }
}

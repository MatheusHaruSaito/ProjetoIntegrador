using ProjetoIntegradorAPI.Models;

namespace ProjetoIntegradorAPI.Repositories.UserRepostory
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<bool> UserTriggerActive(Guid Id);
    }
}

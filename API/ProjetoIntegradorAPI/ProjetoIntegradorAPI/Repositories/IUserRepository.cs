using ProjetoIntegradorAPI.Models;

namespace ProjetoIntegradorAPI.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<bool> UserTriggerActive(Guid Id);
    }
}

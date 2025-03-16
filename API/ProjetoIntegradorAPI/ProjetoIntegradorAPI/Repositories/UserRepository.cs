using ProjetoIntegradorAPI.Context;
using ProjetoIntegradorAPI.Models;

namespace ProjetoIntegradorAPI.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
        {
            
        }
    }
}

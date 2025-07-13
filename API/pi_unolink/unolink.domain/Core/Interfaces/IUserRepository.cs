using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unolink.domain.Models;

namespace unolink.domain.Core.Interfaces
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<List<User>> GetAll();
        Task<User> GetByEmailAsync(string email);
        Task<bool> UseTriggerActive(Guid id);
        
    }
}
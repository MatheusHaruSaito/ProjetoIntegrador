using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unolink.domain.Models;

namespace unolink.domain.Core.Interfaces
{
    public interface IUserPostRepository: IRepository<UserPost, Guid>
    {
        Task<List<UserPost>> GetAll();
        Task<bool> UseTriggerActive(Guid id);

    }
}

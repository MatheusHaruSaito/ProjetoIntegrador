using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unolink.domain.Enums;
using unolink.domain.Models;
using unolink.domain.ValueObjects;

namespace unolink.domain.Core.Interfaces
{
    public interface ISearchRepository
    {
        Task<(List<User> User, List<UserPost> Posts, PaginationData pagination)> SearchAsync(SearchType type, string q, PaginationData paginationData);

    }
}

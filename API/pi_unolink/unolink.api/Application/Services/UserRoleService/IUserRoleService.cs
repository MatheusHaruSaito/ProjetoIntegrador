using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unolink.api.Application.Models.Dtos;

namespace unolink.api.Application.Services.UserRoleService
{
    public interface IUserRoleService
    {
        List<UserRoleDTO> GetAll();
    }
}
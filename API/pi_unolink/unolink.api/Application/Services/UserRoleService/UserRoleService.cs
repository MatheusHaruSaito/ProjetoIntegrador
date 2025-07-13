using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unolink.api.Application.Models.Dtos;
using unolink.domain.Models;

namespace unolink.api.Application.Services.UserRoleService
{
    public class UserRoleService : IUserRoleService
    {
        public List<UserRoleDTO> GetAll()
        {
            var users = new List<UserRoleDTO>();

            foreach (var user in Enum.GetValues(typeof(UserRoleEnum)))
            {
                users.Add(new UserRoleDTO
                {
                    Name = user.ToString(),
                    Value = (int)user
                });
            }
            return users;
        }
    }
}
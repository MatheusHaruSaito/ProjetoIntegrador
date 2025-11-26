using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unolink.api.Application.Models.Dtos;
using unolink.api.Application.Models.Request;

namespace unolink.api.Application.Services.UserService
{
    public interface IUserService
    {
        Task<bool> Add(CreateUserRequest request);
        Task<List<UserDTO>> GetAll();
        Task<UserDTO> GetById(Guid id);
        Task<UserDTO> GetByEmail(string Email);
        Task<bool> Update(UpdateUserRequest request,string baseUrl);
        Task<bool> UserTriggerActive(Guid id);
    }
}
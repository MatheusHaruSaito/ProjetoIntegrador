using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using unolink.api.Application.Models.Dtos;
using unolink.api.Application.Models.Request;
using unolink.domain.Core.Interfaces;
using unolink.domain.Models;

namespace unolink.api.Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        public async Task<bool> Add(CreateUserRequest request)
        {
            var verificarUser = await _userRepository.GetByEmailAsync(request.Email);
            if (verificarUser != null)
            {
                return false;
            }
            
            var user = new User(request.Name, request.Email,
            " ", request.Description, request.Cep);
            await _userManager.AddToRoleAsync(user, "Default");
            user.PasswordHash =  new PasswordHasher<User>().HashPassword(user, request.Password);

            _userRepository.Add(user);

            return await _userRepository.UnitOfWork.SaveEntitiesAsync();
        }

        public async Task<List<UserDTO>> GetAll()
        {
            var data = await _userRepository.GetAll();

            var userDto = data.Select(x => new UserDTO
            {
                Id = x.Id,
                Role = _userManager.GetRolesAsync(x).Result,
                Name = x.UserName,
                Email = x.Email,
                Password = x.PasswordHash,
                Description = x.Description,
                Cep = x.Cep,
                IsActive = x.IsActive,
                CreationDate = x.CreatedAt.ToString("dd-MM-yyyy")    
            }).ToList();

            return userDto;
        }

        public async Task<UserDTO> GetByEmail(string Email)
        {
            var user = await _userRepository.GetByEmailAsync(Email);
            if(user is null)
            {
                return null;
            }
            var userDto = new UserDTO
            {
                Id = user.Id,
                Role = _userManager.GetRolesAsync(user).Result,
                Name = user.UserName,
                Email = user.Email,
                Password = user.PasswordHash,
                Description = user.Description,
                Cep = user.Cep,
                IsActive = user.IsActive,
                CreationDate = user.CreatedAt.ToString("dd-MM-yyyy")
            };
            return userDto;
        }

        public async Task<UserDTO> GetById(Guid id)
        {
            var user = await _userRepository.GetById(id);
            if (user is null)
                return null;
            var userDto = new UserDTO
            {
                Id = user.Id,
                Role = _userManager.GetRolesAsync(user).Result,
                Name = user.UserName,
                Email = user.Email,
                Password = user.PasswordHash,
                Description = user.Description,
                Cep = user.Cep,
                IsActive = user.IsActive,
                CreationDate = user.CreatedAt.ToString("dd-MM-yyyy")
            };
            return userDto;
        }

        public async Task<bool> Update(UpdateUserRequest request)
        {
            var user = await _userRepository.GetById(request.Id);

            if (user is null) return false;

            user.Update(request.Name, request.Email, "");

            user.PasswordHash =  new PasswordHasher<User>().HashPassword(user, request.Password);

            return await _userRepository.UnitOfWork.SaveEntitiesAsync();

        }

        public async Task<bool> UserTriggerActive(Guid id)
        {
            var result = await _userRepository.UseTriggerActive(id);

            if (!result) return false;

            await _userRepository.UnitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
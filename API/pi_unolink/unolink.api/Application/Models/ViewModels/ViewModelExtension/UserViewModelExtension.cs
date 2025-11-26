using System.Reflection.Metadata.Ecma335;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unolink.api.Application.Models.Dtos;

namespace unolink.api.Application.Models.ViewModels.ViewModelExtension
{
    public static class UserViewModelExtension
    {
        public static UserViewModel ToViewModel(this UserDTO DTO)
        {
            return new UserViewModel
            {
                Id = DTO.Id,
                Role = DTO.Role,
                UserPosts = DTO.UserPosts,
                Name = DTO.Name,
                Email = DTO.Email,
                Password = DTO.Password,
                Description = DTO.Description,
                Cep = DTO.Cep,
                IsActive = DTO.IsActive,
                CreationDate = DTO.CreationDate,
                ProfileImgPath = DTO.ProfileImgPath,
            };
        }
        public static UserProfileViewModel ToProfileViewModel(this UserDTO DTO)
        {
            return new UserProfileViewModel
            {
                Name = DTO.Name,
                Email = DTO.Email,
                UserPosts = DTO.UserPosts,
                Description = DTO.Description,
                Cep = DTO.Cep,
                IsActive = DTO.IsActive,
                CreationDate = DTO.CreationDate,
                ProfileImgPath = DTO.ProfileImgPath,

            };
        }
    }
}
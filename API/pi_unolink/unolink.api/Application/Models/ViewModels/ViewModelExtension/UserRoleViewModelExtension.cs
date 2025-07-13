using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unolink.api.Application.Models.Dtos;

namespace unolink.api.Application.Models.ViewModels.ViewModelExtension
{
    public static class UserRoleViewModelExtension
    {
        public static UserRoleViewModel ToViewModel(this UserRoleDTO DTO)
        {
            return new UserRoleViewModel
            {
                Name = DTO.Name,
                Value = DTO.Value
            };
        }
    }
}
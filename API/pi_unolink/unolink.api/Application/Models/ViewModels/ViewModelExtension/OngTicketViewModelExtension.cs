using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unolink.api.Application.Models.Dtos;

namespace unolink.api.Application.Models.ViewModels.ViewModelExtension
{
    public static class OngTicketViewModelExtension
    {
        public static OngTicketViewModel ToViewModel(this OngTicketDTO DTO)
        {
            return new OngTicketViewModel
            {
                Id = DTO.Id,
                Description = DTO.Description,
                Name = DTO.Name,
                Email = DTO.Email,
                Cep = DTO.Cep,
                Cnpj = DTO.Cnpj,
                CreationDate = DTO.CreationDate,
                ExpirationDate = DTO.ExpirationDate
            };
        }
    }
}
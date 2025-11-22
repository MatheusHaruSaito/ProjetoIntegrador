using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unolink.api.Application.Models.Dtos;
using unolink.api.Application.Models.Request.OngTicket;
using unolink.domain.Core.Interfaces;
using unolink.domain.Models;

namespace unolink.api.Application.Services.OngTicketService
{
    public class OngTicketService : IOngTicketService
    {
        private readonly UserManager<User> _userManager;
        private readonly IOngTicketRepository _ongTicketRepository;
        public OngTicketService(IOngTicketRepository ongTicketRepository, UserManager<User> userManager)
        {
            _ongTicketRepository = ongTicketRepository;
            _userManager = userManager;
        }

        public async Task<bool> AcceptTicket(Guid id)
        {
            var ong = await _ongTicketRepository.GetById(id);
            if (ong == null) return false;

            ong.AcceptedOng();

            var user = new User(ong.Name, ong.Email, "", ong.Description, ong.Cep);
            _ongTicketRepository.Update(ong);
            await _userManager.CreateAsync(user);
            await _userManager.AddToRoleAsync(user, "Ong");
             return await _ongTicketRepository.UnitOfWork.SaveEntitiesAsync();
            
        }

        public async Task<bool> Add(CreateOngTicketRequest request)
        {
            var ong = new OngTicket(request.Description, request.Name, request.Email, request.Cep, request.Cnpj);

            _ongTicketRepository.Add(ong);

            return await _ongTicketRepository.UnitOfWork.SaveEntitiesAsync();
        }

        public async Task<bool> DeclineTicket(Guid id)
        {
            var ong = await _ongTicketRepository.GetById(id);

            if (ong is null) return false;

            ong.ReviwedOng();

            _ongTicketRepository.Update(ong);

            await _ongTicketRepository.UnitOfWork.SaveChangesAsync();

            return true;

        }

        public async Task<List<OngTicketDTO>> GetAll()
        {
            var data = await _ongTicketRepository.GetAll();

            var ongTicketDto = data.Select(x => new OngTicketDTO
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                Email = x.Email,
                Cep = x.Cep,
                Cnpj = x.Cnpj,
                CreationDate = x.CreatedAt.ToString("dd-MM-yyyy"),
                ExpirationDate = x.ExpirationDate.ToString("dd-MM-yyyy")
            }).ToList();

            return ongTicketDto;
        }

        public async Task<OngTicketDTO> GetById(Guid id)
        {
            var ongTicket = await _ongTicketRepository.GetById(id);
            if (ongTicket is null)
                return null;

            var ongTicketDto = new OngTicketDTO
            {
                Id = ongTicket.Id,
                Description = ongTicket.Description,
                Name = ongTicket.Name,
                Email = ongTicket.Email,
                Cep = ongTicket.Cep,
                Cnpj = ongTicket.Cnpj,
                CreationDate = ongTicket.CreatedAt.ToString("dd-MM-yyyy"),
                ExpirationDate = ongTicket.ExpirationDate.ToString("dd-MM-yyyy")
            };

            return ongTicketDto;
        }
    }
}
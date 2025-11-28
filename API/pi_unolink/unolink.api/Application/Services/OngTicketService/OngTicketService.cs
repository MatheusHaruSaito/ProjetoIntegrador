using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unolink.api.Application.Models.Dtos;
using unolink.api.Application.Models.Request;
using unolink.api.Application.Models.Request.OngTicket;
using unolink.api.Application.Services.EmailService;
using unolink.domain.Core.Interfaces;
using unolink.domain.Models;
using unolink.infrastructure.Repositories;

namespace unolink.api.Application.Services.OngTicketService
{
    public class OngTicketService : IOngTicketService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IOngTicketRepository _ongTicketRepository;
        private readonly IEmailService _emailService;
        public OngTicketService(IOngTicketRepository ongTicketRepository, UserManager<User> userManager, IUserRepository userRepository, IEmailService emailService)
        {
            _ongTicketRepository = ongTicketRepository;
            _userManager = userManager;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<bool> AcceptTicket(Guid id)
        {
            var ong = await _ongTicketRepository.GetById(id);
            if (ong == null) return false;
            if (ong.Reviwed) return false;
            ong.AcceptedOng();

            string password = Random.Shared.Next(5125, 99999).ToString();
            password += "PasSwOrd";
            var user = new User(ong.Name, ong.Email, password, ong.Description, ong.Cep);
            _ongTicketRepository.Update(ong);
            var userCreated = await _userManager.CreateAsync(user,password);
            if (!userCreated.Succeeded)
            {
                return false;
            }
            var addedRole = await _userManager.AddToRoleAsync(user, "Ong");
            if (!addedRole.Succeeded)
            {
                return false;
            }
            await _ongTicketRepository.UnitOfWork.SaveEntitiesAsync();

            var email = new CreateEmailRequest()
            {
                ToEmail = ong.Email,
                Subject = "Requisição de Ong Confirmada",
                Body = $"Sua requisição de conta de ong foi aceita\n Conta: {ong.Email}\n Senha {password}",
            };
            await _emailService.EmailSender(email);

            if ((await _ongTicketRepository.GetById(id)).Reviwed)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Add(CreateOngTicketRequest request)
        {
            var UserExisits = await _userRepository.GetByEmailAsync(request.Email) is not null;
            var TicketExists = await _ongTicketRepository.GetByEmail(request.Email) is not null;
            if (UserExisits || TicketExists) return false;
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

            var email = new CreateEmailRequest()
            {
                ToEmail = ong.Email,
                Subject = "Requisição de Ong Negada",
                Body = $"Apos analise, podemos concluir que sua requisição é ilegivel para uma conta ong",
            };
            await _emailService.EmailSender(email);

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
                Reviwed = x.Reviwed,
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
                Reviwed = ongTicket.Reviwed,
                CreationDate = ongTicket.CreatedAt.ToString("dd-MM-yyyy"),
                ExpirationDate = ongTicket.ExpirationDate.ToString("dd-MM-yyyy")
            };

            return ongTicketDto;
        }
    }
}
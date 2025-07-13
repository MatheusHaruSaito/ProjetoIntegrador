using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unolink.api.Application.Models.Dtos;
using unolink.api.Application.Models.Request.OngTicket;

namespace unolink.api.Application.Services.OngTicketService
{
    public interface IOngTicketService
    {
        Task<bool> Add(CreateOngTicketRequest request);
        Task<List<OngTicketDTO>> GetAll();
        Task<OngTicketDTO> GetById(Guid id);
        Task<bool> AcceptTicket(Guid id);
        Task<bool> DeclineTicket(Guid id);

    }
}
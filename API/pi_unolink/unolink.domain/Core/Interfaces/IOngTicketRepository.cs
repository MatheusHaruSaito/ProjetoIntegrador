using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unolink.domain.Models;

namespace unolink.domain.Core.Interfaces
{
    public interface IOngTicketRepository : IRepository<OngTicket, Guid>
    {
        //Task<OngTicket> AcceptTicket(Guid id);
        //Task<OngTicket> DeclineTicket(Guid id);
        Task<List<OngTicket>> GetAll();
        Task<OngTicket> GetByEmail(string email);
        void AddUser(User user);
    }
}
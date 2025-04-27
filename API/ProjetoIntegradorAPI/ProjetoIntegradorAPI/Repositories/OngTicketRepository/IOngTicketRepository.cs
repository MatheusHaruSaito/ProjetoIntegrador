using ProjetoIntegradorAPI.Models;

namespace ProjetoIntegradorAPI.Repositories.OngTicketRepository
{
    public interface IOngTicketRepository : IBaseRepository<OngTicket>
    {
        Task<OngTicket> AcceptTicket(Guid Id);
        Task<OngTicket> DeclineTicket(Guid Id);

    }
}

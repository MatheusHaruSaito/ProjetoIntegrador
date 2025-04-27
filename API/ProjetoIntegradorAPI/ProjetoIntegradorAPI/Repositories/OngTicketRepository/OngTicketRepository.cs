using ProjetoIntegradorAPI.Context;
using ProjetoIntegradorAPI.Models;

namespace ProjetoIntegradorAPI.Repositories.OngTicketRepository
{
    public class OngTicketRepository: BaseRepository<OngTicket>, IOngTicketRepository
    {
        public OngTicketRepository(ApplicationDataContext applicationDataContext): base(applicationDataContext)
        { 
        }

        public async Task<OngTicket> AcceptTicket(Guid Id)
        {
            OngTicket ongTicket=  await GetByIdAsync(Id);
            if(ongTicket is null)
            {
                return null;
            }
            ongTicket.Accpeted = true;
            ongTicket.Reviwed = true;
            _applicationDataContext.OngTicket.Update(ongTicket);
            await _applicationDataContext.SaveChangesAsync();
            return ongTicket;
        }

        public async Task<OngTicket> DeclineTicket(Guid Id)
        {
            OngTicket ongTicket = await GetByIdAsync(Id);
            if (ongTicket is null)
            {
                return null;
            }
            ongTicket.Accpeted = false;
            ongTicket.Reviwed = true;
            _applicationDataContext.OngTicket.Update(ongTicket);
            await _applicationDataContext.SaveChangesAsync();
            return ongTicket;
        }
    }
}

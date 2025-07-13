using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using unolink.domain.Core.Interfaces;
using unolink.domain.Models;
using unolink.infrastructure.Context;

namespace unolink.infrastructure.Repositories
{
    public class OngTicketRepository(IUnitOfWork unitOfWork, ApplicationDataContext context) : IRepository<OngTicket, Guid>, IOngTicketRepository
    {
        private readonly ApplicationDataContext _context = context;
        private readonly DbSet<OngTicket> _entity = context.Set<OngTicket>();
        public IUnitOfWork UnitOfWork => unitOfWork;

       
        public void Add(OngTicket entity)
        {
            _entity.Add(entity);
        }

        public void AddUser(User user)
        {
            _context.User.Add(user);
        }

        public Task<OngTicket> DeclineTicket(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OngTicket>> GetAll()
        {
            return await _entity.ToListAsync();
        }

        public async Task<OngTicket?> GetById(Guid id)
        {
            return await _entity.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(OngTicket entity)
        {
            _entity.Update(entity);
        }
    }
}